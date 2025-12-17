using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalData.Queries.GetProductTechnicalData
{
    public class GetProductTechnicalDataQueryHandler
        : IRequestHandler<GetProductTechnicalDataQuery, ProductTechnicalDataDto>
    {
        private readonly IProductTechnicalDataRepository _repository;

        public GetProductTechnicalDataQueryHandler(IProductTechnicalDataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ProductTechnicalDataDto> Handle(
            GetProductTechnicalDataQuery request,
            CancellationToken cancellationToken)
        {
            var productData = await _repository.GetByIdWithDetailsAsync(request.ProductTechnicalDataId);

            if (productData == null)
            {
                throw new InvalidOperationException(
                    $"Product technical data with ID '{request.ProductTechnicalDataId}' not found.");
            }

            return productData.ToDto(); // Extension method handles all mapping
        }
    }
}
