using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate
{
    public class ProductTechnicalData
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid TemplateId { get; private set; }
        public string Version { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private readonly List<ProductTechnicalDataProperty> _properties = new();
        public IReadOnlyCollection<ProductTechnicalDataProperty> Properties => _properties.AsReadOnly();

        // Navigation
        public Product Product { get; private set; }
        public TechnicalDataTemplate Template { get; private set; }

        private ProductTechnicalData() { }

        public static ProductTechnicalData CreateFromTemplate(
            Guid productId,
            Guid templateId,
            string version)
        {
            return new ProductTechnicalData
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                TemplateId = templateId,
                Version = version,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void SetPropertyValue(Guid templatePropertyId, string value)
        {
            var property = _properties.FirstOrDefault(p => p.TemplatePropertyId == templatePropertyId);

            if (property == null)
            {
                property = ProductTechnicalDataProperty.Create(Id, templatePropertyId, value);
                _properties.Add(property);
            }
            else
            {
                property.UpdateValue(value);
            }

            UpdatedAt = DateTime.UtcNow;
        }

        public void AddCustomProperty(string name, string value, string semanticId = null)
        {
            var customProperty = ProductTechnicalDataProperty.CreateCustom(
                Id, name, value, semanticId);
            _properties.Add(customProperty);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
