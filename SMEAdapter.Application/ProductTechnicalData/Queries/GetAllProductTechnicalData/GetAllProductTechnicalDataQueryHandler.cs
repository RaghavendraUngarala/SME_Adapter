using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalData.Queries.GetAllProductTechnicalData
{
    public class GetAllProductTechnicalDataQueryHandler
        : IRequestHandler<GetAllProductTechnicalDataQuery, List<ProductTechnicalDataSummaryDto>>
    {
        private readonly IProductTechnicalDataRepository _repository;

        public GetAllProductTechnicalDataQueryHandler(IProductTechnicalDataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<ProductTechnicalDataSummaryDto>> Handle(
            GetAllProductTechnicalDataQuery request,
            CancellationToken cancellationToken)
        {
            var allData = await _repository.GetAllAsync();

            return allData
                .Select(d => d.ToSummaryDto()) // ← Now this will work!
                .OrderByDescending(d => d.UpdatedAt ?? d.CreatedAt)
                .ToList();
        }
    }
}
