using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMEAdapter.Application.ProductDocuments.AddProductDocuments;
using SMEAdapter.Application.DTOs;


namespace SMEAdapter.Application.ProductDocuments.AddProductDocuments
{

    public class AddProductDocumentCommand : IRequest<Guid>
    {
        public ProductDocumentDto ProductDocument { get; }
        public AddProductDocumentCommand(ProductDocumentDto productDocument)
        {
            ProductDocument = productDocument;
        }
    }

}
