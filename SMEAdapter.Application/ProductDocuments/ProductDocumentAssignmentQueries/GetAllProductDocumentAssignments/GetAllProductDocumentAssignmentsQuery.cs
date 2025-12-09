using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SMEAdapter.Application.DTOs;

namespace SMEAdapter.Application.ProductDocuments.ProductDocumentAssignmentQueries.GetAllProductDocumentAssignments
{
    public  record GetAllProductDocumentAssignmentsQuery (): IRequest<IReadOnlyList<ProductDocumentAssignmentDto>>;
   
}
