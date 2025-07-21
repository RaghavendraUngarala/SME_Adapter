using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMEAdapter.Application.Products.GetProducts;


namespace SMEAdapter.Application.Products.GetProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            // map Domain -> DTO
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ManufacturerName = p.ManufacturerName ?? string.Empty,
                SerialNumber = p.SerialNumber ?? string.Empty,

                AddressInfo = new AddressInfo
                {
                    ZipCode = p.AddressInfo?.ZipCode ?? string.Empty,
                    City = p.AddressInfo?.City ?? string.Empty,
                    Country = p.AddressInfo?.Country ?? string.Empty
                },

                ProductInfo = new ProductInfo
                {
                    ProductDesignation = p.ProductInfo?.ProductDesignation ?? string.Empty,
                    ProductRoot = p.ProductInfo?.ProductRoot ?? string.Empty,
                    ProductFamily = p.ProductInfo?.ProductFamily ?? string.Empty,
                    ProductType = p.ProductInfo?.ProductType ?? string.Empty,
                    OrderCode = p.ProductInfo?.OrderCode ?? string.Empty,
                    ArticleNumber = p.ProductInfo?.ArticleNumber ?? string.Empty
                }
            }).ToList();

            return productDtos;
        }
    }
}
