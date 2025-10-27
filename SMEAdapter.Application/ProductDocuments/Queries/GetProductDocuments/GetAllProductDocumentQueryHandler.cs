using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductDocuments.Queries.GetProductDocuments
{
    public class GetAllProductDocumentsQueryHandler : IRequestHandler<GetAllProductDocumentsQuery, List<ProductDocumentDto>>
    {
        private readonly IProductDocumentRepository _repository;

        public GetAllProductDocumentsQueryHandler(IProductDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDocumentDto>> Handle(GetAllProductDocumentsQuery request, CancellationToken cancellationToken)
        {
            var documents = await _repository.GetByProductIdAsync(request.ProductId, cancellationToken);

            return documents.Select(d => new ProductDocumentDto
            {
                Id = d.Id,
                ProductId = d.ProductId!,
                FileName = d.FileName,
                ContentType = d.ContentType,
                Data = d.Data,

                // Version
                Language = d.Version?.Language,
                Version = d.Version?.Version,
                Title = d.Version?.Title,
                Summary = d.Version?.Summary,
                Keywords = d.Version?.Keywords,
                State = d.Version?.State,
                StateDate = d.Version?.StateDate,
                OrganisationName = d.Version?.OrganisationName,
                OrganisationOfficialName = d.Version?.OrganisationOfficialName,

                // Identifier
                ValueId = d.Identifier?.ValueId,
                DomainId = d.Identifier?.DomainId,

                // Classification
                ClassificationSystem = d.Classification?.ClassificationSystem,
                ClassName = d.Classification?.ClassName,
                ClassLang = d.Classification?.ClassLang,
                ClassDescription = d.Classification?.ClassDescription,
                ClassId = d.Classification?.ClassId
            }).ToList();
        }
    }
}
