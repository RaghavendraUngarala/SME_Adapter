using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SMEAdapter.Application.DTOs;



namespace SMEAdapter.Application.ProductDocuments.Queries.GetProductDocumentsById
{
    public class GetProductDocumentByIdQueryHandler : IRequestHandler<GetProductDocumentByIdQuery, ProductDocumentDto>
    {
        private readonly IProductDocumentRepository _productDocumentRepository;

        public GetProductDocumentByIdQueryHandler(IProductDocumentRepository productDocumentRepository)
        {
            _productDocumentRepository = productDocumentRepository;
        }

        public async Task<ProductDocumentDto> Handle(GetProductDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _productDocumentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return null;
            var dto = new ProductDocumentDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId!,
                FileName = entity.FileName,
                ContentType = entity.ContentType,
                Data = entity.Data,

                // Version
                Language = entity.Version?.Language,
                Version = entity.Version?.Version,
                Title = entity.Version?.Title,
                Summary = entity.Version?.Summary,
                Keywords = entity.Version?.Keywords,
                State = entity.Version?.State,
                StateDate = entity.Version?.StateDate,
                OrganisationName = entity.Version?.OrganisationName,
                OrganisationOfficialName = entity.Version?.OrganisationOfficialName,

                // Identifier
                ValueId = entity.Identifier?.ValueId,
                DomainId = entity.Identifier?.DomainId,

                // Classification
                ClassificationSystem = entity.Classification?.ClassificationSystem,
                ClassName = entity.Classification?.ClassName,
                ClassLang = entity.Classification?.ClassLang,
                ClassDescription = entity.Classification?.ClassDescription,
                ClassId = entity.Classification?.ClassId
            };
            return dto;
        }
    }
}
