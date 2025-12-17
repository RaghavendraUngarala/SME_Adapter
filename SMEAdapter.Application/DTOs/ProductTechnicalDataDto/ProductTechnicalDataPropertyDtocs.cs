using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs.ProductTechnicalDataDto
{
    public class ProductTechnicalDataPropertyDto
    {
        public Guid Id { get; set; }
        public Guid ProductTechnicalDataId { get; set; }
        public Guid? TemplatePropertyId { get; set; }
        public string Value { get; set; }
        public bool IsCustomProperty { get; set; }

        // For custom properties
        public string CustomPropertyName { get; set; }
        public string CustomSemanticId { get; set; }

        // From template property (for display)
        public string PropertyName { get; set; }
        public string SemanticId { get; set; }
        public string DataType { get; set; }
        public string Unit { get; set; }
        public string SectionName { get; set; }
        public int SectionOrder { get; set; }
        public int PropertyOrder { get; set; }

        // Display helpers
        public string DisplayName => IsCustomProperty ? CustomPropertyName : PropertyName;
        public string DisplayValue => string.IsNullOrEmpty(Unit)
            ? Value
            : $"{Value} {Unit}";
    }
}
