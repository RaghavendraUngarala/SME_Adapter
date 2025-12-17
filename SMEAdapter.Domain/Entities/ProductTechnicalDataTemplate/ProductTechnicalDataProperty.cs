using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate
{
    public class ProductTechnicalDataProperty
    {
        public Guid Id { get; private set; }
        public Guid ProductTechnicalDataId { get; private set; }
        public Guid? TemplatePropertyId { get; private set; } // Null for custom properties
        public string Value { get; private set; }
        public bool IsCustomProperty { get; private set; }

        // For custom properties only
        public string CustomPropertyName { get; private set; }
        public string CustomSemanticId { get; private set; }

        // Navigation
        public ProductTechnicalData ProductTechnicalData { get; private set; }
        public TemplateProperty TemplateProperty { get; private set; }

        private ProductTechnicalDataProperty() { }

        public static ProductTechnicalDataProperty Create(
            Guid productTechnicalDataId,
            Guid templatePropertyId,
            string value)
        {
            return new ProductTechnicalDataProperty
            {
                Id = Guid.NewGuid(),
                ProductTechnicalDataId = productTechnicalDataId,
                TemplatePropertyId = templatePropertyId,
                Value = value,
                IsCustomProperty = false
            };
        }

        public static ProductTechnicalDataProperty CreateCustom(
            Guid productTechnicalDataId,
            string name,
            string value,
            string semanticId)
        {
            return new ProductTechnicalDataProperty
            {
                Id = Guid.NewGuid(),
                ProductTechnicalDataId = productTechnicalDataId,
                Value = value,
                IsCustomProperty = true,
                CustomPropertyName = name,
                CustomSemanticId = semanticId
            };
        }

        public void UpdateValue(string value)
        {
            Value = value;
        }
    }
}
