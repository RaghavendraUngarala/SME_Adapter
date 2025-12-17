using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs.ProductTechnicalDataDto
{
    public class TemplateSectionDto
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public string Name { get; set; }
        public string SemanticId { get; set; }
        public int Order { get; set; }

        // Navigation properties
        public List<TemplatePropertyDto> Properties { get; set; } = new();

        // Computed properties
        public int PropertyCount => Properties?.Count ?? 0;
        public int RequiredPropertyCount => Properties?.Count(p => p.IsRequired) ?? 0;
    }
}
