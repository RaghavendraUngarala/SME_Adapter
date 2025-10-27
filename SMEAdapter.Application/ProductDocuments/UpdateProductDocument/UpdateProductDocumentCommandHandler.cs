using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
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

            // --- Update main document fields ---
            existing.ProductId = dto.ProductId;
            existing.FileName = dto.FileName ?? existing.FileName;
            existing.ContentType = dto.ContentType ?? existing.ContentType;

            // Only replace file data if new data is provided
            if (dto.Data is not null && dto.Data.Length > 0)
                existing.Data = dto.Data;

            // --- Update nested Version ---
            if (existing.Version == null)
                existing.Version = new DocumentVersion();

            existing.Version.Language = dto.Language;
            existing.Version.Version = dto.Version;
            existing.Version.Title = dto.Title;
            existing.Version.Summary = dto.Summary;
            existing.Version.Keywords = dto.Keywords;
            existing.Version.State = dto.State;
            existing.Version.StateDate = dto.StateDate ?? DateTime.UtcNow;
            existing.Version.OrganisationName = dto.OrganisationName;
            existing.Version.OrganisationOfficialName = dto.OrganisationOfficialName;

            // --- Update nested Identifier ---
            if (existing.Identifier == null)
                existing.Identifier = new DocumentIdentifier();

            existing.Identifier.ValueId = dto.ValueId;
            existing.Identifier.DomainId = dto.DomainId;

            // --- Update nested Classification ---
            if (existing.Classification == null)
                existing.Classification = new DocumentClassification();

            existing.Classification.ClassificationSystem = dto.ClassificationSystem;
            existing.Classification.ClassName = dto.ClassName;
            existing.Classification.ClassLang = dto.ClassLang;
            existing.Classification.ClassDescription = dto.ClassDescription;
            existing.Classification.ClassId = dto.ClassId;

            // Save changes through repository
            await _repository.UpdateAsync(existing, cancellationToken);

            return Unit.Value;
        }
    }
}
