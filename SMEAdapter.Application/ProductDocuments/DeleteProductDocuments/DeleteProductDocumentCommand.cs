using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductDocuments.DeleteProductDocuments
{
    public record DeleteProductDocumentCommand(Guid ProductId, List<Guid> DocumentIds) : IRequest<Unit>;

}
