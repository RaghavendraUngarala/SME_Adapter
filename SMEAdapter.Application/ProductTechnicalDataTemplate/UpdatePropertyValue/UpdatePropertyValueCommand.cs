using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.UpdatePropertyValue
{
    public record UpdatePropertyValueCommand : IRequest<Unit>
    {
        public Guid ProductTechnicalDataId { get; init; }
        public Guid TemplatePropertyId { get; init; }
        public string Value { get; init; }
    }
}
