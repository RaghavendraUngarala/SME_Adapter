using MediatR;
using SMEAdapter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductDocuments.Queries.GetAllSharedDocuments
{
    public record GetAllSharedDocumentsQuery()
    : IRequest<IReadOnlyList<ProductDocumentDto>>;
}
