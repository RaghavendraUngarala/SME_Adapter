using MediatR;

using SMEAdapter.Application.DTOs;


namespace SMEAdapter.Application.ProductDocuments.AddProductDocuments
{

    public class CreateProductDocumentCommand : IRequest<Guid>
    {
        public ProductDocumentDto ProductDocument { get; }
        public CreateProductDocumentCommand(ProductDocumentDto productDocument)
        {
            ProductDocument = productDocument;
        }
    }

}
