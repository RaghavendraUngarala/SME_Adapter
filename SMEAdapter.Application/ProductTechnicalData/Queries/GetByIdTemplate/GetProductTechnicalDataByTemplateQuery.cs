using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalData.Queries.GetByIdTemplate
{
    public record GetProductTechnicalDataByTemplateQuery(Guid TemplateId)
        : IRequest<List<ProductTechnicalDataSummaryDto>>;
}
