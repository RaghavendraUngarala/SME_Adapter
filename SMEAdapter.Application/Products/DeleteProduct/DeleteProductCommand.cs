using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Products.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public List<Guid> ProductIds { get; }

        public DeleteProductCommand(List<Guid> productIds)
        {
            ProductIds = productIds;
        }
    }

}
