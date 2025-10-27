using MediatR;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace SMEAdapter.Application.ProductDocuments.DeleteProductDocuments
{
    public class DeleteProductDocumentCommandHandler : IRequestHandler<DeleteProductDocumentCommand, Unit>
    {
        private readonly IProductDocumentRepository _repository;

        public DeleteProductDocumentCommandHandler(IProductDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductDocumentCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.DocumentIds)
            {
                await _repository.DeleteAsync(id, cancellationToken);
                Console.WriteLine("Deleting IDs: " + string.Join(", ", request.DocumentIds));
            }

            await _repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
