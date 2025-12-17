using MediatR;
using SMEAdapter.Application.DTOs.ProductTechnicalDataDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.Queries.GetAllTemplateById
{
    public record GetTemplateByIdQuery(Guid TemplateId) : IRequest<TechnicalDataTemplateDto>;
}
