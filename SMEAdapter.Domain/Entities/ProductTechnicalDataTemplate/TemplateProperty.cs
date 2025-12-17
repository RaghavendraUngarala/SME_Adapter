using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate
{
    public class TemplateProperty
    {
        public Guid Id { get; private set; }
        public Guid SectionId { get; private set; }
        public string Name { get; private set; }
        public string SemanticId { get; private set; }
        public string DataType { get; private set; } // String, Number, Boolean, etc.
        public string Unit { get; private set; } // e.g., "mm", "kg", "V"
        public bool IsRequired { get; private set; }
        public int Order { get; private set; }
        public string DefaultValue { get; private set; }

        // Navigation
        public TemplateSection Section { get; private set; }

        private TemplateProperty() { }

        public static TemplateProperty Create(
            string name,
            string semanticId,
            string dataType,
            string unit,
            bool isRequired,
            int order,
            string defaultValue = null)
        {
            return new TemplateProperty
            {
                Id = Guid.NewGuid(),
                Name = name,
                SemanticId = semanticId,
                DataType = dataType,
                Unit = unit,
                IsRequired = isRequired,
                Order = order,
                DefaultValue = defaultValue
            };
        }
    }
}
