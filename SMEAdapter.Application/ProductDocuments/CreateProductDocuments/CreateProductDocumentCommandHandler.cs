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

        public async Task<Guid> Handle(CreateProductDocumentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ProductDocument;

        
            var document = new ProductDocument(
                dto.FileName,
                dto.ContentType,
                dto.Data ?? Array.Empty<byte>()
            );

            
            document.SetProduct(dto.ProductId);

          
            document.UpdateVersion(
                dto.Language,
                dto.Version,
                dto.Title,
                dto.Summary,
                dto.Keywords,
                dto.State,
                dto.StateDate,
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

            // STEP 6: Save
            await _repository.AddAsync(document, cancellationToken);

            return document.Id;
        }
    }
}