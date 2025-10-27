using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SMEAdapter.Application.DTOs;

namespace SMEAdapter.Application.ProductDocuments.Queries.GetProductDocumentsById
{
   public class GetProductDocumentByIdQuery : IRequest<ProductDocumentDto>
   {
        public Guid Id { get; set; }
        public GetProductDocumentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
