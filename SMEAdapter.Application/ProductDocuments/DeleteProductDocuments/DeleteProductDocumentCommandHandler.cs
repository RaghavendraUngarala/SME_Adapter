using MediatR;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace SMEAdapter.Application.ProductDocuments.DeleteProductDocuments
{
    public class DeleteProductDocumentCommandHandler : IRequestHandler<DeleteProductDocumentCommand, Unit>
    {
        private readonly IProductDocumentRepository _repo;

        public DeleteProductDocumentCommandHandler(IProductDocumentRepository repository)
        {
            _repo = repository;
        }

        public async Task<Unit> Handle(DeleteProductDocumentCommand request, CancellationToken ct)
        {

            Guid productId = request.ProductId;
            foreach (var id in request.DocumentIds)
            {
                var doc = await _repo.GetByIdAsync(id, ct);
                if (doc == null)
                    continue;

                // ----------------------------------
                // CASE 1: OWNED DOCUMENT
                // ----------------------------------
                if (doc.OwnershipType == ProductDocument.DocumentOwnershipType.Owned)
                {
                    // Remove assignment (if present)
                    if (doc.ProductId.HasValue)
                        await _repo.RemoveAssignmentAsync(doc.ProductId.Value, doc.Id, ct);

                    // Delete the document itself
                    await _repo.DeleteAsync(doc.Id, ct);

                    continue;
                }

                
                    await _repo.RemoveAssignmentAsync(request.ProductId, doc.Id, ct);
            }

            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
