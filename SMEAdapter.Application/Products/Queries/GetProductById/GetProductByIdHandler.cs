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

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var p = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (p is null) return null;

            // Helper to convert a LangStringSet -> Dictionary
            static Dictionary<string, string> ToDict(SMEAdapter.Domain.ValueObjects.LangStringSet lss) =>
                lss.Items.ToDictionary(x => x.Language, x => x.Text, StringComparer.OrdinalIgnoreCase);

            var dto = new ProductDto
            {
                Id = p.Id,
                ManufacturerName = ToDict(p.ManufacturerName),
                SerialNumber = ToDict(p.SerialNumber),
                ImageUrl = p.ImageUrl,

                ProductInfo = new ProductInfoDto
                {
                    ProductDesignation = ToDict(p.ProductInfo.ProductDesignation),
                    ProductRoot = ToDict(p.ProductInfo.ProductRoot),
                    ProductFamily = ToDict(p.ProductInfo.ProductFamily),
                    ProductType = ToDict(p.ProductInfo.ProductType),
                    OrderCode = ToDict(p.ProductInfo.OrderCode),
                    ArticleNumber = ToDict(p.ProductInfo.ArticleNumber)
                },

                AddressInfo = new AddressInfoDto
                {
                    Street = ToDict(p.AddressInfo.Street),
                    ZipCode = ToDict(p.AddressInfo.ZipCode),
                    City = ToDict(p.AddressInfo.City),
                    Country = ToDict(p.AddressInfo.Country)
                }
            };

            return dto;
        }
    }
}
