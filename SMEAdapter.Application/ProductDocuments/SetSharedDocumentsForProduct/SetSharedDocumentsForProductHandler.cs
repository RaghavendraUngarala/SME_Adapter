using MediatR;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductDocuments.SetSharedDocumentsForProduct
{
    public class SetSharedDocumentsForProductCommandHandler
       : IRequestHandler<SetSharedDocumentsForProductCommand, Unit>
    {
        private readonly IProductDocumentRepository _repo;

        public SetSharedDocumentsForProductCommandHandler(IProductDocumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(SetSharedDocumentsForProductCommand request, CancellationToken ct)
        {
           
            await _repo.AddSharedDocumentsForProductAsync(
                request.ProductId,
                request.DocumentIds,
                ct
            );

            return Unit.Value;
        }
    }
}
