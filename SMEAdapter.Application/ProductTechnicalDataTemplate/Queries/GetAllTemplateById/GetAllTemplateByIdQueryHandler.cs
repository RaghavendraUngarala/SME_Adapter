using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.Queries.GetAllTemplateById
{
    public class GetTemplateByIdQueryHandler
        : IRequestHandler<GetTemplateByIdQuery, TechnicalDataTemplateDto>
    {
        private readonly ITechnicalDataTemplateRepository _repository;

        public GetTemplateByIdQueryHandler(ITechnicalDataTemplateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<TechnicalDataTemplateDto> Handle(
            GetTemplateByIdQuery request,
            CancellationToken cancellationToken)
        {
            var template = await _repository.GetByIdWithDetailsAsync(request.TemplateId);

            if (template == null)
            {
                throw new InvalidOperationException(
                    $"Template with ID '{request.TemplateId}' not found.");
            }

            return template.ToDto(); // Extension method
        }
    }
}
