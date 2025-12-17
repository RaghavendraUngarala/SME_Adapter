using MediatR;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.DeleteTemplate
{
    public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, Unit>
    {
        private readonly ITechnicalDataTemplateRepository _repository;

        public DeleteTemplateCommandHandler(ITechnicalDataTemplateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            // Repository DeleteAsync already checks if template is in use
            // and throws InvalidOperationException if it is
            await _repository.DeleteAsync(request.TemplateId);

            return Unit.Value;
        }
    }
}
