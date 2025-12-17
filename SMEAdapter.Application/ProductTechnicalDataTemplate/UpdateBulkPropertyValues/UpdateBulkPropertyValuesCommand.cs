using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.NewFolder
{
    public record UpdateBulkPropertyValuesCommand : IRequest<Unit>
    {
        public Guid ProductTechnicalDataId { get; init; }
        public Dictionary<Guid, string> PropertyValues { get; init; } = new();
    }
}
