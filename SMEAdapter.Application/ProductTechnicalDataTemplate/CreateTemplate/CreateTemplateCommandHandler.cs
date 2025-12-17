using MediatR;
using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.CreateTemplate
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, Guid>
    {
        private readonly ITechnicalDataTemplateRepository _repository;

        public CreateTemplateCommandHandler(ITechnicalDataTemplateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Guid> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            // Validate: Check if template name already exists
            if (await _repository.ExistsByNameAsync(request.Name))
            {
                throw new InvalidOperationException(
                    $"A template with the name '{request.Name}' already exists.");
            }

            // Validate: Sections must have unique names
            var duplicateSections = request.Sections
                .GroupBy(s => s.Name.ToLower())
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateSections.Any())
            {
                throw new InvalidOperationException(
                    $"Duplicate section names found: {string.Join(", ", duplicateSections)}");
            }

            // Create template
            var template = TechnicalDataTemplate.Create(
                request.Name,
                request.Description,
                request.Version,
                request.IdtaSubmodelId);

            // Add sections and properties
            foreach (var sectionDto in request.Sections.OrderBy(s => s.Order))
            {
                // Validate: Properties within section must have unique names
                var duplicateProps = sectionDto.Properties
                    .GroupBy(p => p.Name.ToLower())
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateProps.Any())
                {
                    throw new InvalidOperationException(
                        $"Duplicate property names in section '{sectionDto.Name}': {string.Join(", ", duplicateProps)}");
                }

                var section = TemplateSection.Create(
                    sectionDto.Name,
                    sectionDto.SemanticId,
                    sectionDto.Order);

                foreach (var propertyDto in sectionDto.Properties.OrderBy(p => p.Order))
                {
                    var property = TemplateProperty.Create(
                        propertyDto.Name,
                        propertyDto.SemanticId,
                        propertyDto.DataType,
                        propertyDto.Unit,
                        propertyDto.IsRequired,
                        propertyDto.Order,
                        propertyDto.DefaultValue);

                    section.AddProperty(property);
                }

                template.AddSection(section);
            }

            // Save to database
            await _repository.AddAsync(template);

            return template.Id;
        }
    }
}
