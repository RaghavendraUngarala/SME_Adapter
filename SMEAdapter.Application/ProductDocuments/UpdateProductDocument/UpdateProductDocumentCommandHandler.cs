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
using static SMEAdapter.Domain.Entities.ProductDocument;


namespace SMEAdapter.Application.ProductDocuments.UpdateProductDocument
{
    public class UpdateProductDocumentCommandHandler : IRequestHandler<UpdateProductDocumentCommand, Unit>
    {
        private readonly IProductDocumentRepository _repo;

        public UpdateProductDocumentCommandHandler(IProductDocumentRepository repository)
        {
            _repo = repository;
        }

        public async Task<Unit> Handle(UpdateProductDocumentCommand request, CancellationToken ct)
        {
            var dto = request.ProductDocument;

            if (!dto.ProductId.HasValue)
                throw new InvalidOperationException("ProductId is required when updating a document.");

            var productId = dto.ProductId.Value;

         
            // 1. Load existing
          
            var existing = await _repo.GetByIdAsync(dto.Id, ct);
            if (existing == null)
                throw new KeyNotFoundException($"Document '{dto.Id}' not found");

            bool wasShared = existing.ProductId == null;
            bool becomesOwned = dto.OwnershipType == ProductDocument.DocumentOwnershipType.Owned;
            bool becomesShared = dto.OwnershipType == ProductDocument.DocumentOwnershipType.Shared;

           
            // 2. Helper: apply metadata updates
         
            void ApplyMetadataUpdates(ProductDocument doc)
            {
                // File replacement
                if (dto.Data != null && dto.Data.Length > 0)
                    doc.Data = dto.Data;

                doc.FileName = dto.FileName ?? doc.FileName;
                doc.ContentType = dto.ContentType ?? doc.ContentType;

                var title = LangStringSet.FromDictionary(dto.Title);
                var subtitle = LangStringSet.FromDictionary(dto.SubTitle);
                var desc = LangStringSet.FromDictionary(dto.Description);
                var keywords = LangStringSet.FromDictionary(dto.Keywords);

                doc.UpdateVersion(
                    dto.Language, dto.Version,
                    title, subtitle, desc, keywords,
                    dto.StatusValue, dto.StatusSetDate,
                    dto.OrganisationName, dto.OrganisationOfficialName
                );

                doc.UpdateIdentifier(dto.ValueId, dto.DomainId);

                var classNameSet = LangStringSet.FromDictionary(dto.ClassName);
                doc.UpdateClassification(dto.ClassificationSystem, classNameSet,
                    dto.ClassLang, dto.ClassDescription, dto.ClassId);
            }

            
            // CASE 1: SHARED → OWNED  (CLONE LOGIC)
         
            if (wasShared && becomesOwned)
            {
                // Create clone
                var clone = new ProductDocument(existing.FileName, existing.ContentType, existing.Data);

                // Apply metadata from DTO
                ApplyMetadataUpdates(clone);

                // Assign to this product
                clone.AssignToProduct(productId);

                // Save new document
                await _repo.AddAsync(clone, ct);

                // Remove old assignment
                await _repo.RemoveAssignmentAsync(productId, existing.Id, ct);

                // Assign clone
                await _repo.AddAssignmentAsync(productId, clone.Id, ct);

                return Unit.Value;
            }

          
            // CASE 2: OWNED → SHARED
          
            if (!wasShared && becomesShared)
            {
                ApplyMetadataUpdates(existing);

                // Now the document becomes shared
                existing.MarkAsShared(); // sets ProductId = null

                await _repo.UpdateAsync(existing, ct);

                // Keep assignment in table
                return Unit.Value;
            }

            // ========================================================================
            // CASE 3: OWNED → OWNED
            // ========================================================================
            if (!wasShared && becomesOwned)
            {
                ApplyMetadataUpdates(existing);
                existing.AssignToProduct(productId);

                await _repo.UpdateAsync(existing, ct);
                return Unit.Value;
            }

            // ========================================================================
            // CASE 4: SHARED → SHARED
            // ========================================================================
            if (wasShared && becomesShared)
            {
                ApplyMetadataUpdates(existing);

                existing.MarkAsShared(); // stays shared
                await _repo.UpdateAsync(existing, ct);
                return Unit.Value;
            }

            throw new InvalidOperationException("Unhandled ownership transition.");
        }
    }
}
