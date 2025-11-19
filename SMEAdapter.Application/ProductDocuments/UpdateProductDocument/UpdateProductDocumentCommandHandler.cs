using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMEAdapter.Application.ProductDocuments.UpdateProductDocument
{
    public class UpdateProductDocumentCommandHandler : IRequestHandler<UpdateProductDocumentCommand, Unit>
    {
        private readonly IProductDocumentRepository _repository;

        public UpdateProductDocumentCommandHandler(IProductDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductDocumentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ProductDocument;

            // Fetch existing document
            var existing = await _repository.GetByIdAsync(dto.Id, cancellationToken);
            if (existing == null)
                throw new KeyNotFoundException($"Product document with ID '{dto.Id}' not found.");


            existing.FileName = dto.FileName ?? existing.FileName;
            existing.ContentType = dto.ContentType ?? existing.ContentType;

            // Replace file only if a new one was uploaded
            if (dto.Data is not null && dto.Data.Length > 0)
                existing.Data = dto.Data;


            existing.Version.Language = dto.Language;
            existing.Version.Version = dto.Version;
            existing.Version.Title = dto.Title;
            existing.Version.Summary = dto.Summary;
            existing.Version.Keywords = dto.Keywords;
            existing.Version.State = dto.State;
            existing.Version.StateDate = dto.StateDate ?? existing.Version.StateDate;
            existing.Version.OrganisationName = dto.OrganisationName;
            existing.Version.OrganisationOfficialName = dto.OrganisationOfficialName;

            existing.Identifier.ValueId = dto.ValueId;
            existing.Identifier.DomainId = dto.DomainId;

            
            var classNameSet = LangStringSet.FromDictionary(dto.ClassName);

            existing.UpdateClassification(
                classificationSystem: dto.ClassificationSystem,
                classNameSet,
                classLang: dto.ClassLang,
                classDescription: dto.ClassDescription,
                classId: dto.ClassId
            );

            // Save changes
            await _repository.UpdateAsync(existing, cancellationToken);

            return Unit.Value;
        }
    }
}
