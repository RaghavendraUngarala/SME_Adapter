using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.CreateTemplate
{
    public record CreateTemplateCommand : IRequest<Guid>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Version { get; init; }
        public string IdtaSubmodelId { get; init; }
        public List<CreateTemplateSectionDto> Sections { get; init; } = new();
    }

    public record CreateTemplateSectionDto
    {
        public string Name { get; init; }
        public string SemanticId { get; init; }
        public int Order { get; init; }
        public List<CreateTemplatePropertyDto> Properties { get; init; } = new();
    }

    public record CreateTemplatePropertyDto
    {
        public string Name { get; init; }
        public string SemanticId { get; init; }
        public string DataType { get; init; }
        public string Unit { get; init; }
        public bool IsRequired { get; init; }
        public int Order { get; init; }
        public string DefaultValue { get; init; }
    }
}
