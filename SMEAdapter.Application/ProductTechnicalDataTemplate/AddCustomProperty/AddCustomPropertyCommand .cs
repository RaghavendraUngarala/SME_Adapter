using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.AddCustomProperty
{
    public record AddCustomPropertyCommand : IRequest<Unit>
    {
        public Guid ProductTechnicalDataId { get; init; }
        public string Name { get; init; }
        public string Value { get; init; }
        public string SemanticId { get; init; }
    }
}
