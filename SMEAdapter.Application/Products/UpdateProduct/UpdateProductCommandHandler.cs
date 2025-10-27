using MediatR;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMEAdapter.Domain.Entities;

namespace SMEAdapter.Application.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand,Unit>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Product;

            var existingProduct = await _productRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (existingProduct == null)
                throw new Exception("Product not found.");

            // Update fields
            existingProduct.ManufacturerName = dto.ManufacturerName;
            existingProduct.SerialNumber = dto.SerialNumber;
            existingProduct.ImageUrl = dto.ImageUrl;
            existingProduct.AddressInfo = new AddressInfo
            {
                ZipCode = request.Product.AddressInfo.ZipCode,
                City = request.Product.AddressInfo.City,
                Country = request.Product.AddressInfo.Country
            };

            existingProduct.ProductInfo = new ProductInfo
            {
                ProductDesignation = request.Product.ProductInfo.ProductDesignation,
                ProductRoot = request.Product.ProductInfo.ProductRoot,
                ProductFamily = request.Product.ProductInfo.ProductFamily,
                ProductType = request.Product.ProductInfo.ProductType,
                OrderCode = request.Product.ProductInfo.OrderCode,
                ArticleNumber = request.Product.ProductInfo.ArticleNumber
            };

            await _productRepository.UpdateAsync(existingProduct, cancellationToken);

            return Unit.Value;
        }
    }

}
