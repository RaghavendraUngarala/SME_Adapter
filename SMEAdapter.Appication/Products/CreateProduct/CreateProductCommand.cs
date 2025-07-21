using MediatR;
using SMEAdapter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public ProductDto Product { get; }

        public CreateProductCommand(ProductDto product)
        {
            Product = product;
        }
    }
}
