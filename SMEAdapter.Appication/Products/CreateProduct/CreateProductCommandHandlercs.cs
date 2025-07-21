using MediatR;
using SMEAdapter.Domain;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
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

          
            var product = new Product
            {
                ManufacturerName = dto.ManufacturerName,
                SerialNumber = dto.SerialNumber,
                AddressInfo = new AddressInfo
                {
                    ZipCode = dto.AddressInfo.ZipCode,
                    City = dto.AddressInfo.City,
                    Country = dto.AddressInfo.Country
                },
                ProductInfo = new ProductInfo
                {
                    ProductDesignation = dto.ProductInfo.ProductDesignation,
                    ProductRoot = dto.ProductInfo.ProductRoot,
                    ProductFamily = dto.ProductInfo.ProductFamily,
                    ProductType = dto.ProductInfo.ProductType,
                    OrderCode = dto.ProductInfo.OrderCode,
                    ArticleNumber = dto.ProductInfo.ArticleNumber
                }
            };

            await _productRepository.AddAsync(product, cancellationToken);

            return product.Id;
        }
    }
}