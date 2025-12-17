using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.DeleteTemplate
{
    public record DeleteTemplateCommand(Guid TemplateId) : IRequest<Unit>;
}
