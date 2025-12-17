using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate
{
    public class TemplateSection
    {
        public Guid Id { get; private set; }
        public Guid TemplateId { get; private set; }
        public string Name { get; private set; }
        public string SemanticId { get; private set; } // ECLASS/IEC CDD reference
        public int Order { get; private set; }

        private readonly List<TemplateProperty> _properties = new();
        public IReadOnlyCollection<TemplateProperty> Properties => _properties.AsReadOnly();

        // Navigation
        public TechnicalDataTemplate Template { get; private set; }

        private TemplateSection() { }

        public static TemplateSection Create(
            string name,
            string semanticId,
            int order)
        {
            return new TemplateSection
            {
                Id = Guid.NewGuid(),
                Name = name,
                SemanticId = semanticId,
                Order = order
            };
        }

        public void AddProperty(TemplateProperty property)
        {
            if (_properties.Any(p => p.Name == property.Name))
                throw new InvalidOperationException($"Property '{property.Name}' already exists");

            _properties.Add(property);
        }
    }
}