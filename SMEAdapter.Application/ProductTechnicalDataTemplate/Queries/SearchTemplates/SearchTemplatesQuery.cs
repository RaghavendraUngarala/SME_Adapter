using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.Queries.SearchTemplates
{
    public record SearchTemplatesQuery(string SearchTerm)
        : IRequest<List<TechnicalDataTemplateSummaryDto>>;
}
