using MediatR;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.UpdateTemplate
{
    public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, Unit>
    {
        private readonly ITechnicalDataTemplateRepository _repository;

        public UpdateTemplateCommandHandler(ITechnicalDataTemplateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            // Get template
            var template = await _repository.GetByIdAsync(request.Id);
            if (template == null)
            {
                throw new InvalidOperationException(
                    $"Template with ID '{request.Id}' not found.");
            }

            // Validate: Check if new name conflicts with existing template
            if (await _repository.ExistsByNameAsync(request.Name, request.Id))
            {
                throw new InvalidOperationException(
                    $"Another template with the name '{request.Name}' already exists.");
            }

            // Update
            template.UpdateInfo(request.Name, request.Description, request.Version);

            await _repository.UpdateAsync(template);

            return Unit.Value;
        }
    }
}
