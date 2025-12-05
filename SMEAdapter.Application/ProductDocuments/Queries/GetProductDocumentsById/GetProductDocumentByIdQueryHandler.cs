using SMEAdapter.Domain.Interfaces;
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
            var document = await _productDocumentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (document == null)
                return null;
            static Dictionary<string, string> ToDict(SMEAdapter.Domain.ValueObjects.LangStringSet lss) =>
                lss.Items.ToDictionary(x => x.Language, x => x.Text, StringComparer.OrdinalIgnoreCase);
            var productId =
                    document.ProductId
                    ?? document.DocumentAssignments.FirstOrDefault()?.ProductId;
            return new ProductDocumentDto
            {
                Id = document.Id,
                ProductId = productId!,
                FileName = document.FileName,
                ContentType = document.ContentType,
                Data = document.Data,

                // Version - Fix the ToDictionary calls
                Language = document.Version?.Language,
                Version = document.Version?.Version,
                Title = ToDict(document.Version?.Title),
                SubTitle = ToDict(document.Version?.SubTitle),
                Description = ToDict(document.Version?.Description),
                Keywords = ToDict(document.Version?.Keywords),
                StatusValue = document.Version?.StatusValue,
                StatusSetDate = document.Version?.StatusSetDate,
                OrganisationName = document.Version?.OrganisationName,
                OrganisationOfficialName = document.Version?.OrganisationOfficialName,

                // Identifier
                ValueId = document.Identifier?.ValueId,
                DomainId = document.Identifier?.DomainId,

                // Classification
                ClassificationSystem = document.Classification?.ClassificationSystem,
                ClassName = ToDict(document.Classification?.ClassName),
                ClassLang = document.Classification?.ClassLang,
                ClassDescription = document.Classification?.ClassDescription,
                ClassId = document.Classification?.ClassId,

                OwnershipType = document.OwnershipType
            };
        }
    }
}
