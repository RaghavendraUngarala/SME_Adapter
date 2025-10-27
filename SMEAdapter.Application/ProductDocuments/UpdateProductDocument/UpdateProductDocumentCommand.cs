using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SMEAdapter.Application.DTOs;

namespace SMEAdapter.Application.ProductDocuments.UpdateProductDocument
{
    public class UpdateProductDocumentCommand : IRequest<Unit>
    {
        public ProductDocumentDto ProductDocument { get; }
        public UpdateProductDocumentCommand(ProductDocumentDto productDocument)
        {
            ProductDocument = productDocument;
        }
    }
}
