using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.Queries.SearchTemplates
{
    public class SearchTemplatesQueryHandler
        : IRequestHandler<SearchTemplatesQuery, List<TechnicalDataTemplateSummaryDto>>
    {
        private readonly ITechnicalDataTemplateRepository _repository;

        public SearchTemplatesQueryHandler(ITechnicalDataTemplateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<TechnicalDataTemplateSummaryDto>> Handle(
            SearchTemplatesQuery request,
            CancellationToken cancellationToken)
        {
            var templates = await _repository.SearchAsync(request.SearchTerm);

            return templates
                .Select(t => t.ToSummaryDto()) // Extension method
                .OrderBy(t => t.Name)
                .ToList();
        }
    }
}
