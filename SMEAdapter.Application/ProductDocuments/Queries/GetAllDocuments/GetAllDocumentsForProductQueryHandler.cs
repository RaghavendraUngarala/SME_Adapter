using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;

namespace SMEAdapter.Application.ProductDocuments.Queries.GetAllDocuments
{
    public class GetAllDocumentsForProductQueryHandler
    : IRequestHandler<GetAllDocumentsForProductQuery, IReadOnlyList<ProductDocumentDto>>
    {
        private readonly IProductDocumentRepository _repo;

        public GetAllDocumentsForProductQueryHandler(IProductDocumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<ProductDocumentDto>> Handle(
            GetAllDocumentsForProductQuery request,
            CancellationToken cancellationToken)
        {
            var owned = await _repo.GetByProductIdAsync(request.ProductId, cancellationToken);
            var shared = await _repo.GetSharedForProductAsync(request.ProductId, cancellationToken);

            var all = owned.Concat(shared).DistinctBy(d => d.Id).ToList();

            static Dictionary<string, string> ToDict(SMEAdapter.Domain.ValueObjects.LangStringSet? lss) =>
                lss?.Items.ToDictionary(x => x.Language, x => x.Text, StringComparer.OrdinalIgnoreCase)
                ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var dtos = all.Select(entity => new ProductDocumentDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                FileName = entity.FileName,
                ContentType = entity.ContentType,
                Data = entity.Data,
                OwnershipType = entity.OwnershipType,

                Language = entity.Version?.Language,
                Version = entity.Version?.Version,
                Title = ToDict(entity.Version?.Title),
                SubTitle = ToDict(entity.Version?.SubTitle),
                Description = ToDict(entity.Version?.Description),
                Keywords = ToDict(entity.Version?.Keywords),
                StatusValue = entity.Version?.StatusValue,
                StatusSetDate = entity.Version?.StatusSetDate,
                OrganisationName = entity.Version?.OrganisationName,
                OrganisationOfficialName = entity.Version?.OrganisationOfficialName,

                ValueId = entity.Identifier?.ValueId,
                DomainId = entity.Identifier?.DomainId,

                ClassificationSystem = entity.Classification?.ClassificationSystem,
                ClassName = ToDict(entity.Classification?.ClassName),
                ClassLang = entity.Classification?.ClassLang,
                ClassDescription = entity.Classification?.ClassDescription,
                ClassId = entity.Classification?.ClassId,

                
            }).ToList();

            return dtos;
        }
    }

}
