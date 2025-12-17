using MediatR;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.AssignTemplateToProduct
{
    public class AssignTemplateToProductCommandHandler
         : IRequestHandler<AssignTemplateToProductCommand, Guid>
    {
        private readonly IProductTechnicalDataRepository _technicalDataRepository;
        private readonly ITechnicalDataTemplateRepository _templateRepository;

        public AssignTemplateToProductCommandHandler(
            IProductTechnicalDataRepository technicalDataRepository,
            ITechnicalDataTemplateRepository templateRepository)
        {
            _technicalDataRepository = technicalDataRepository
                ?? throw new ArgumentNullException(nameof(technicalDataRepository));
            _templateRepository = templateRepository
                ?? throw new ArgumentNullException(nameof(templateRepository));
        }

        public async Task<Guid> Handle(
            AssignTemplateToProductCommand request,
            CancellationToken cancellationToken)
        {
            // Validate: Check if product already has technical data
            if (await _technicalDataRepository.ExistsForProductAsync(request.ProductId))
            {
                throw new InvalidOperationException(
                    $"Product with ID '{request.ProductId}' already has technical data assigned. " +
                    $"Use update commands to modify existing data.");
            }

            // Get template with full details to validate
            var template = await _templateRepository.GetByIdWithDetailsAsync(request.TemplateId);
            if (template == null)
            {
                throw new InvalidOperationException(
                    $"Template with ID '{request.TemplateId}' not found.");
            }

            // Create product technical data instance
            // FIXED: Use the entity from Domain.Entities directly
            var productTechnicalData = Domain.Entities.ProductTechnicalDataTemplate.ProductTechnicalData.CreateFromTemplate(
                request.ProductId,
                request.TemplateId,
                template.Version);

            // Validate required properties
            var requiredProperties = template.Sections
                .SelectMany(s => s.Properties)
                .Where(p => p.IsRequired)
                .ToList();

            var missingRequired = new List<string>();

            foreach (var required in requiredProperties)
            {
                if (!request.PropertyValues.ContainsKey(required.Id) ||
                    string.IsNullOrWhiteSpace(request.PropertyValues[required.Id]))
                {
                    var sectionName = template.Sections
                        .FirstOrDefault(s => s.Properties.Any(p => p.Id == required.Id))?.Name
                        ?? "Unknown";
                    missingRequired.Add($"{required.Name} (Section: {sectionName})");
                }
            }

            if (missingRequired.Any())
            {
                throw new InvalidOperationException(
                    $"The following required properties are missing or empty:\n" +
                    string.Join("\n", missingRequired.Select(p => $"  - {p}")));
            }

            // Set property values
            foreach (var propertyValue in request.PropertyValues)
            {
                if (!string.IsNullOrWhiteSpace(propertyValue.Value))
                {
                    productTechnicalData.SetPropertyValue(propertyValue.Key, propertyValue.Value);
                }
            }

            // Save to database
            await _technicalDataRepository.AddAsync(productTechnicalData);

            return productTechnicalData.Id;
        }
    }
}
