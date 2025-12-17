using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs.ProductTechnicalDataDto
{
    public class TemplatePropertyDto
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public string Name { get; set; }
        public string SemanticId { get; set; }
        public string DataType { get; set; }
        public string Unit { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public string DefaultValue { get; set; }

        // Display helpers
        public string DisplayName => $"{Name}{(IsRequired ? " *" : "")}";
        public string FullDisplayName => string.IsNullOrEmpty(Unit)
            ? DisplayName
            : $"{DisplayName} [{Unit}]";
    }
}
