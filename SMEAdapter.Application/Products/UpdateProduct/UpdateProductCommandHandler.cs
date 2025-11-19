using MediatR;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var existing = await _productRepository.GetByIdAsync(dto.Id, cancellationToken)
                           ?? throw new Exception("Product not found.");

            // Replace aggregates / value objects (read-only props)
            existing.ReplaceManufacturerName(LangStringSet.FromDictionary(dto.ManufacturerName));
            existing.ReplaceSerialNumber(LangStringSet.FromDictionary(dto.SerialNumber));
            existing.SetImageUrl(dto.ImageUrl);

            existing.ReplaceProductInfo(new ProductInfo(
                LangStringSet.FromDictionary(dto.ProductInfo.ProductDesignation),
                LangStringSet.FromDictionary(dto.ProductInfo.ProductRoot),
                LangStringSet.FromDictionary(dto.ProductInfo.ProductFamily),
                LangStringSet.FromDictionary(dto.ProductInfo.ProductType),
                LangStringSet.FromDictionary(dto.ProductInfo.OrderCode),
                LangStringSet.FromDictionary(dto.ProductInfo.ArticleNumber)
            ));

            existing.ReplaceAddress(new AddressInfo(
                LangStringSet.FromDictionary(dto.AddressInfo.Street),
                LangStringSet.FromDictionary(dto.AddressInfo.ZipCode),
                LangStringSet.FromDictionary(dto.AddressInfo.City),
                LangStringSet.FromDictionary(dto.AddressInfo.Country)
            ));

            await _productRepository.UpdateAsync(existing, cancellationToken);
            return Unit.Value;
        }
    }

}
