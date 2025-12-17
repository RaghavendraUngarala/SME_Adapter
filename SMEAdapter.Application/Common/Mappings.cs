using SMEAdapter.Application.DTOs;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;

using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMEAdapter.Application.Common.Mappings
{
    /// <summary>
    /// Extension methods for mapping domain entities to DTOs (Manual mapping - no AutoMapper needed)
    /// </summary>
    public static class EntityExtensions
    {
        #region Template Mappings

        /// <summary>
        /// Maps TechnicalDataTemplate entity to full DTO with sections and properties
        /// </summary>
        public static TechnicalDataTemplateDto ToDto(this TechnicalDataTemplate entity)
        {
            if (entity == null) return null;

            return new TechnicalDataTemplateDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Version = entity.Version,
                IdtaSubmodelId = entity.IdtaSubmodelId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Sections = entity.Sections?.Select(s => s.ToDto()).ToList() ?? new List<TemplateSectionDto>()
            };
        }

        /// <summary>
        /// Maps TechnicalDataTemplate entity to summary DTO (lightweight for lists)
        /// </summary>
        public static TechnicalDataTemplateSummaryDto ToSummaryDto(
            this TechnicalDataTemplate entity,
            int usageCount = 0)
        {
            if (entity == null) return null;

            return new TechnicalDataTemplateSummaryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Version = entity.Version,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                SectionCount = entity.Sections?.Count ?? 0,
                PropertyCount = entity.Sections?.Sum(s => s.Properties?.Count ?? 0) ?? 0,
                UsageCount = usageCount
            };
        }

        /// <summary>
        /// Maps TemplateSection entity to DTO
        /// </summary>
        public static TemplateSectionDto ToDto(this TemplateSection entity)
        {
            if (entity == null) return null;

            return new TemplateSectionDto
            {
                Id = entity.Id,
                TemplateId = entity.TemplateId,
                Name = entity.Name,
                SemanticId = entity.SemanticId,
                Order = entity.Order,
                Properties = entity.Properties?.Select(p => p.ToDto()).ToList() ?? new List<TemplatePropertyDto>()
            };
        }

        /// <summary>
        /// Maps TemplateProperty entity to DTO
        /// </summary>
        public static TemplatePropertyDto ToDto(this TemplateProperty entity)
        {
            if (entity == null) return null;

            return new TemplatePropertyDto
            {
                Id = entity.Id,
                SectionId = entity.SectionId,
                Name = entity.Name,
                SemanticId = entity.SemanticId,
                DataType = entity.DataType,
                Unit = entity.Unit,
                IsRequired = entity.IsRequired,
                Order = entity.Order,
                DefaultValue = entity.DefaultValue
            };
        }

        /// <summary>
        /// Maps TechnicalDataTemplate to lightweight reference DTO
        /// </summary>
        public static TemplateReferenceDto ToReferenceDto(this TechnicalDataTemplate entity)
        {
            if (entity == null) return null;

            return new TemplateReferenceDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Version = entity.Version
            };
        }

        #endregion

        #region Product Technical Data Mappings

        /// <summary>
        /// Maps ProductTechnicalData entity to full DTO with all property values
        /// </summary>
        public static ProductTechnicalDataDto ToDto(this Domain.Entities.ProductTechnicalDataTemplate.ProductTechnicalData entity)
        {
            if (entity == null) return null;

            return new ProductTechnicalDataDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                TemplateId = entity.TemplateId,
                Version = entity.Version,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Product = entity.Product?.ToProductDto(),
                Template = entity.Template?.ToReferenceDto(),
                Properties = entity.Properties?
                    .Select(p => p.ToDto(entity.Template))
                    .OrderBy(p => p.SectionOrder)
                    .ThenBy(p => p.PropertyOrder)
                    .ToList() ?? new List<ProductTechnicalDataPropertyDto>()
            };
        }

        /// <summary>
        /// Maps ProductTechnicalData entity to summary DTO (lightweight for lists)
        /// </summary>
        public static ProductTechnicalDataSummaryDto ToSummaryDto(this Domain.Entities.ProductTechnicalDataTemplate.ProductTechnicalData entity)
        {
            if (entity == null) return null;

            var totalProps = entity.Properties?.Count ?? 0;
            var filledProps = entity.Properties?.Count(p => !string.IsNullOrEmpty(p.Value)) ?? 0;
            var completionPercentage = totalProps > 0
                ? Math.Round((double)filledProps / totalProps * 100, 2)
                : 0;

            return new ProductTechnicalDataSummaryDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                ProductName = entity.Product?.GetProductName(),
                TemplateName = entity.Template?.Name,
                TemplateVersion = entity.Version,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CompletionPercentage = completionPercentage
            };
        }

        /// <summary>
        /// Maps ProductTechnicalDataProperty entity to DTO with metadata
        /// </summary>
        public static ProductTechnicalDataPropertyDto ToDto(
            this ProductTechnicalDataProperty entity,
            TechnicalDataTemplate template = null)
        {
            if (entity == null) return null;

            var dto = new ProductTechnicalDataPropertyDto
            {
                Id = entity.Id,
                ProductTechnicalDataId = entity.ProductTechnicalDataId,
                TemplatePropertyId = entity.TemplatePropertyId,
                Value = entity.Value,
                IsCustomProperty = entity.IsCustomProperty,
                CustomPropertyName = entity.CustomPropertyName,
                CustomSemanticId = entity.CustomSemanticId
            };

            // Map from template property if available
            if (!entity.IsCustomProperty && entity.TemplateProperty != null)
            {
                dto.PropertyName = entity.TemplateProperty.Name;
                dto.SemanticId = entity.TemplateProperty.SemanticId;
                dto.DataType = entity.TemplateProperty.DataType;
                dto.Unit = entity.TemplateProperty.Unit;
                dto.PropertyOrder = entity.TemplateProperty.Order;

                // Find section information
                if (template != null)
                {
                    var section = template.Sections?
                        .FirstOrDefault(s => s.Id == entity.TemplateProperty.SectionId);

                    if (section != null)
                    {
                        dto.SectionName = section.Name;
                        dto.SectionOrder = section.Order;
                    }
                }
                else if (entity.TemplateProperty.Section != null)
                {
                    dto.SectionName = entity.TemplateProperty.Section.Name;
                    dto.SectionOrder = entity.TemplateProperty.Section.Order;
                }
            }
            else if (entity.IsCustomProperty)
            {
                dto.SemanticId = entity.CustomSemanticId;
                dto.SectionOrder = int.MaxValue; // Custom properties at the end
            }

            return dto;
        }

        #endregion

        #region Product Mappings

        /// <summary>
        /// Maps Product entity to ProductDto (existing complex DTO with multi-language support)
        /// </summary>
        public static ProductDto ToProductDto(this Product entity)
        {
            if (entity == null) return null;

            return new ProductDto
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                ImageUrl = entity.ImageUrl,
                ManufacturerName = entity.ManufacturerName?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                SerialNumber = entity.SerialNumber?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                ProductInfo = entity.ProductInfo?.ToProductInfoDto() ?? new ProductInfoDto(),
                AddressInfo = entity.AddressInfo?.ToAddressInfoDto() ?? new AddressInfoDto()
            };
        }

        /// <summary>
        /// Maps ProductInfo value object to ProductInfoDto
        /// </summary>
        public static ProductInfoDto ToProductInfoDto(this ProductInfo entity)
        {
            if (entity == null) return new ProductInfoDto();

            return new ProductInfoDto
            {
                ProductDesignation = entity.ProductDesignation?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                ProductRoot = entity.ProductRoot?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                ProductFamily = entity.ProductFamily?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                ProductType = entity.ProductType?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                OrderCode = entity.OrderCode?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                ArticleNumber = entity.ArticleNumber?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            };
        }

        /// <summary>
        /// Maps AddressInfo value object to AddressInfoDto
        /// </summary>
        public static AddressInfoDto ToAddressInfoDto(this AddressInfo entity)
        {
            if (entity == null) return new AddressInfoDto();

            return new AddressInfoDto
            {
                Street = entity.Street?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                ZipCode = entity.ZipCode?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                City = entity.City?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                Country = entity.Country?.ToDictionary() ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            };
        }

        /// <summary>
        /// Helper to get product name from Product entity (for summary displays)
        /// Returns first available language or "Unknown Product"
        /// </summary>
        public static string GetProductName(this Product entity)
        {
            if (entity == null) return "Unknown Product";

            // Try to get product designation from ProductInfo
            var designation = entity.ProductInfo?.ProductDesignation?.ToDictionary();
            if (designation != null && designation.Any())
            {
                // Try English first, then any language
                return designation.GetValueOrDefault("en")
                    ?? designation.GetValueOrDefault("EN")
                    ?? designation.FirstOrDefault().Value
                    ?? "Unknown Product";
            }

            return "Unknown Product";
        }

        #endregion

        #region Collection Mappings

        /// <summary>
        /// Maps collection of TechnicalDataTemplate to DTOs
        /// </summary>
        public static List<TechnicalDataTemplateDto> ToDtoList(
            this IEnumerable<TechnicalDataTemplate> entities)
        {
            return entities?.Select(e => e.ToDto()).ToList() ?? new List<TechnicalDataTemplateDto>();
        }

        /// <summary>
        /// Maps collection of TechnicalDataTemplate to summary DTOs
        /// </summary>
        public static List<TechnicalDataTemplateSummaryDto> ToSummaryDtoList(
            this IEnumerable<TechnicalDataTemplate> entities)
        {
            return entities?.Select(e => e.ToSummaryDto()).ToList() ?? new List<TechnicalDataTemplateSummaryDto>();
        }

        
        public static List<ProductTechnicalDataDto> ToDtoList(
            this IEnumerable<Domain.Entities.ProductTechnicalDataTemplate.ProductTechnicalData> entities)
        {
            return entities?.Select(e => e.ToDto()).ToList() ?? new List<ProductTechnicalDataDto>();
        }

        
        public static List<ProductTechnicalDataSummaryDto> ToSummaryDtoList(
            this IEnumerable<Domain.Entities.ProductTechnicalDataTemplate.ProductTechnicalData> entities)
        {
            return entities?.Select(e => e.ToSummaryDto()).ToList() ?? new List<ProductTechnicalDataSummaryDto>();
        }

        #endregion
    }
}