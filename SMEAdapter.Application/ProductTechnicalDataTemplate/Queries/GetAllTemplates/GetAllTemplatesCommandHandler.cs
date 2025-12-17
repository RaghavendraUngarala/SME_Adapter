using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.Queries.GetAllTemplates
{
    public class GetAllTemplatesQueryHandler
        : IRequestHandler<GetAllTemplatesQuery, List<TechnicalDataTemplateSummaryDto>>
    {
        private readonly ITechnicalDataTemplateRepository _repository;

        public GetAllTemplatesQueryHandler(ITechnicalDataTemplateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<TechnicalDataTemplateSummaryDto>> Handle(
            GetAllTemplatesQuery request,
            CancellationToken cancellationToken)
        {
            var templates = await _repository.GetAllWithSectionCountAsync();

            var dtos = new List<TechnicalDataTemplateSummaryDto>();

            foreach (var template in templates)
            {
                var usageCount = await _repository.GetUsageCountAsync(template.Id);
                var dto = template.ToSummaryDto(usageCount); // Extension method
                dtos.Add(dto);
            }

            return dtos.OrderBy(t => t.Name).ToList();
        }
    }
}
