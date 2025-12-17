using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalData.Queries.GetProductTechnicalDataByProductId
{
    public class GetProductTechnicalDataByProductIdQueryHandler
        : IRequestHandler<GetProductTechnicalDataByProductIdQuery, ProductTechnicalDataDto>
    {
        private readonly IProductTechnicalDataRepository _repository;

        public GetProductTechnicalDataByProductIdQueryHandler(IProductTechnicalDataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ProductTechnicalDataDto> Handle(
            GetProductTechnicalDataByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var productData = await _repository.GetByProductIdWithDetailsAsync(request.ProductId);

            if (productData == null)
            {
                throw new InvalidOperationException(
                    $"No technical data found for product with ID '{request.ProductId}'.");
            }

            return productData.ToDto(); // Extension method
        }
    }
}
