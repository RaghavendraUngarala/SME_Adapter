using MediatR;
using SMEAdapter.Domain;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Product;

            var product = new Product(
                manufacturerName: LangStringSet.FromDictionary(dto.ManufacturerName),
               
                productInfo: new ProductInfo(
                    LangStringSet.FromDictionary(dto.ProductInfo.ProductDesignation),
                    LangStringSet.FromDictionary(dto.ProductInfo.ProductRoot),
                    LangStringSet.FromDictionary(dto.ProductInfo.ProductFamily),
                    LangStringSet.FromDictionary(dto.ProductInfo.ProductType),
                    LangStringSet.FromDictionary(dto.ProductInfo.OrderCode),
                    LangStringSet.FromDictionary(dto.ProductInfo.ArticleNumber)

                ),
                addressInfo: new AddressInfo(
                    LangStringSet.FromDictionary(dto.AddressInfo.Street),
                    LangStringSet.FromDictionary(dto.AddressInfo.ZipCode),
                    LangStringSet.FromDictionary(dto.AddressInfo.City),
                    LangStringSet.FromDictionary(dto.AddressInfo.Country)
                ),
                serialNumber: LangStringSet.FromDictionary(dto.SerialNumber),
                imageUrl: dto.ImageUrl
            );
            if (dto.CompanyId.HasValue)
            {
                product.SetCompany(dto.CompanyId.Value);
            }

            await _productRepository.AddAsync(product, cancellationToken);
            return product.Id;
        }
    }
}