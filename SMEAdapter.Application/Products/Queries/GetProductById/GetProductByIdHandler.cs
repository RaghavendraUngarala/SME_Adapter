using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
                return null;

            var dto = new ProductDto
            {
                Id = product.Id,
                ManufacturerName = product.ManufacturerName ?? string.Empty,
                SerialNumber = product.SerialNumber ?? string.Empty,
                ImageUrl = product.ImageUrl ?? string.Empty,
                AddressInfo = new AddressInfo
                {
                    ZipCode = product.AddressInfo?.ZipCode ?? string.Empty,
                    City = product.AddressInfo?.City ?? string.Empty,
                    Country = product.AddressInfo?.Country ?? string.Empty
                },

                ProductInfo = new ProductInfo
                {
                    ProductDesignation = product.ProductInfo?.ProductDesignation ?? string.Empty,
                    ProductRoot = product.ProductInfo?.ProductRoot ?? string.Empty,
                    ProductFamily = product.ProductInfo?.ProductFamily ?? string.Empty,
                    ProductType = product.ProductInfo?.ProductType ?? string.Empty,
                    OrderCode = product.ProductInfo?.OrderCode ?? string.Empty,
                    ArticleNumber = product.ProductInfo?.ArticleNumber ?? string.Empty
                }
            };

            return dto;
        }
    }
}
