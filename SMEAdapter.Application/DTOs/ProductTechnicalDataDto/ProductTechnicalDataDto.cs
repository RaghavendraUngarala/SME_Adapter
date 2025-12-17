using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs.ProductTechnicalDataDto
{
    public class ProductTechnicalDataDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid TemplateId { get; set; }
        public string Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ProductDto Product { get; set; }
        public TemplateReferenceDto Template { get; set; }
        public List<ProductTechnicalDataPropertyDto> Properties { get; set; } = new();

        // Computed properties
        public int TotalPropertyCount => Properties?.Count ?? 0;
        public int FilledPropertyCount => Properties?.Count(p => !string.IsNullOrEmpty(p.Value)) ?? 0;
        public int CustomPropertyCount => Properties?.Count(p => p.IsCustomProperty) ?? 0;
        public double CompletionPercentage => TotalPropertyCount > 0
            ? (double)FilledPropertyCount / TotalPropertyCount * 100
            : 0;
    }
}
