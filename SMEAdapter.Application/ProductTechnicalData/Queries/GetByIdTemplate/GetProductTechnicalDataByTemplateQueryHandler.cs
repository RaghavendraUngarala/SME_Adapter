using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalData.Queries.GetByIdTemplate
{
    public class GetProductTechnicalDataByTemplateQueryHandler
       : IRequestHandler<GetProductTechnicalDataByTemplateQuery, List<ProductTechnicalDataSummaryDto>>
    {
        private readonly IProductTechnicalDataRepository _repository;

        public GetProductTechnicalDataByTemplateQueryHandler(IProductTechnicalDataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<ProductTechnicalDataSummaryDto>> Handle(
            GetProductTechnicalDataByTemplateQuery request,
            CancellationToken cancellationToken)
        {
            var dataList = await _repository.GetByTemplateIdAsync(request.TemplateId);

            return dataList
                .Select(d => d.ToSummaryDto()) // Extension method
                .OrderBy(d => d.ProductName)
                .ToList();
        }
    }
}
