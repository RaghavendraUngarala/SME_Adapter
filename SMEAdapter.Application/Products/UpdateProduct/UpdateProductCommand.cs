using MediatR;
using SMEAdapter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public ProductDto Product { get; }

        public UpdateProductCommand(ProductDto product)
        {
            Product = product;
        }
    }
   
}
