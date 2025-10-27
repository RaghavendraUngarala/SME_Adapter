using MediatR;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Products.DeleteProduct
{
    public class DeleteProductDocumentCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductDocumentCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.ProductIds)
            {
                var product = await _repository.GetByIdAsync(id);
                if (product is not null)
                    await _repository.DeleteAsync(product);
            }

            return Unit.Value;
        }
    }
}
