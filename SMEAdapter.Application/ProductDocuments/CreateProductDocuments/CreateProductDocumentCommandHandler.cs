using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMEAdapter.Application.ProductDocuments.AddProductDocuments
{
    public class CreateProductDocumentHandler : IRequestHandler<CreateProductDocumentCommand, Guid>
    {
        private readonly IProductDocumentRepository _repository;

        public CreateProductDocumentHandler(IProductDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateProductDocumentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ProductDocument;

            var document = new ProductDocument
            {
                ProductId = dto.ProductId,
                FileName = dto.FileName,
                ContentType = dto.ContentType,
                Data = dto.Data ?? Array.Empty<byte>(),

                Version = new DocumentVersion
                {
                    Language = dto.Language,
                    Version = dto.Version,
                    Title = dto.Title,
                    Summary = dto.Summary,
                    Keywords = dto.Keywords,
                    State = dto.State,
                    StateDate = dto.StateDate,
                    OrganisationName = dto.OrganisationName,
                    OrganisationOfficialName = dto.OrganisationOfficialName
                },

                Identifier = new DocumentIdentifier
                {
                    ValueId = dto.ValueId,
                    DomainId = dto.DomainId
                },

                Classification = new DocumentClassification
                {
                    ClassificationSystem = dto.ClassificationSystem,
                    ClassName = dto.ClassName,
                    ClassLang = dto.ClassLang,
                    ClassDescription = dto.ClassDescription,
                    ClassId = dto.ClassId
                }

            };
            
            await _repository.AddAsync(document, cancellationToken);

            return document.Id;
        }
    }
}