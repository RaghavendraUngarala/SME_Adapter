using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.AssignTemplateToProduct
{
    public record AssignTemplateToProductCommand : IRequest<Guid>
    {
        public Guid ProductId { get; init; }
        public Guid TemplateId { get; init; }
        public Dictionary<Guid, string> PropertyValues { get; init; } = new();
    }
}
