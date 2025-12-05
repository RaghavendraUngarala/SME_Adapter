using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductDocuments.Queries.GetAllSharedDocuments
{
    public class GetAllSharedDocumentsQueryHandler
     : IRequestHandler<GetAllSharedDocumentsQuery, IReadOnlyList<ProductDocumentDto>>
    {
        private readonly IProductDocumentRepository _repo;

        public GetAllSharedDocumentsQueryHandler(IProductDocumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<ProductDocumentDto>> Handle(
            GetAllSharedDocumentsQuery request,
            CancellationToken cancellationToken)
        {
            var shared = await _repo.GetAllSharedAsync(cancellationToken);

            static Dictionary<string, string> ToDict(SMEAdapter.Domain.ValueObjects.LangStringSet? lss) =>
                lss?.Items.ToDictionary(x => x.Language, x => x.Text, StringComparer.OrdinalIgnoreCase)
                ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            return shared.Select(d => new ProductDocumentDto
            {
                Id = d.Id,
                ProductId = d.ProductId,
                FileName = d.FileName,
                ContentType = d.ContentType,
                Data = d.Data,

                // Version
                Language = d.Version?.Language,
                Version = d.Version?.Version,
                Title = d.Version?.Title?.ToDictionary(),
                Description = d.Version?.Description?.ToDictionary(),
                SubTitle = d.Version?.SubTitle?.ToDictionary(),
                StatusValue = d.Version?.StatusValue,
                Keywords = d.Version?.Keywords?.ToDictionary(),

                StatusSetDate = d.Version?.StatusSetDate,
                OrganisationName = d.Version?.OrganisationName,
                OrganisationOfficialName = d.Version?.OrganisationOfficialName,

                // Identifier
                ValueId = d.Identifier?.ValueId,
                DomainId = d.Identifier?.DomainId,

                // Classification
                ClassificationSystem = d.Classification?.ClassificationSystem,
                ClassName = d.Classification?.ClassName?.ToDictionary(),
                ClassLang = d.Classification?.ClassLang,
                ClassDescription = d.Classification?.ClassDescription,
                ClassId = d.Classification?.ClassId
            }).ToList();
        }
    }
}
