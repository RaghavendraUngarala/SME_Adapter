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


namespace SMEAdapter.Application.ProductDocuments.AddProductDocuments
{
    public class CreateProductDocumentHandler
         : IRequestHandler<CreateProductDocumentCommand, Guid>
    {
        private readonly IProductDocumentRepository _repository;

        public CreateProductDocumentHandler(IProductDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateProductDocumentCommand request, CancellationToken ct)
        {
            var dto = request.ProductDocument;



            if (!dto.ProductId.HasValue)
                throw new InvalidOperationException("Document must have a ProductId when created.");

            var productId = dto.ProductId.Value;


            var document = new ProductDocument(
                dto.FileName,
                dto.ContentType,
                dto.Data ?? Array.Empty<byte>()
            );

            


            var titleSet = LangStringSet.FromDictionary(dto.Title);
            var subTitleSet = LangStringSet.FromDictionary(dto.SubTitle);
            var descriptionSet = LangStringSet.FromDictionary(dto.Description);
            var keywordsSet = LangStringSet.FromDictionary(dto.Keywords);

            dto.StatusSetDate ??= DateTime.UtcNow;
            document.UpdateVersion(
                dto.Language,
                dto.Version,
                titleSet,
                subTitleSet,
                descriptionSet,
                keywordsSet,
                dto.StatusValue,
                dto.StatusSetDate,
                dto.OrganisationName,
                dto.OrganisationOfficialName
            );

            

            document.UpdateIdentifier(dto.ValueId, dto.DomainId);

           
            var classNameSet = LangStringSet.FromDictionary(dto.ClassName);

            document.UpdateClassification(
                dto.ClassificationSystem,
                classNameSet,
                dto.ClassLang,
                dto.ClassDescription,
                dto.ClassId
            );


            if (dto.OwnershipType == ProductDocument.DocumentOwnershipType.Owned)
            {
                // Owned = tied only to this product
                document.AssignToProduct(productId);
            }
            else
            {
                // Shared = ProductId must be null
                document.MarkAsShared();
            }

            // 7️⃣ Save document first (needs ID)
            await _repository.AddAsync(document, ct);

            // 8️⃣ Always create assignment (both owned and shared)
            await _repository.AddAssignmentAsync(productId, document.Id, ct);

            return document.Id;


            
        }
    }
}