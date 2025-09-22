using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductDocuments.GetProductDocuments
{
    public class GetProductDocumentsHandler : IRequestHandler<GetProductDocumentCommand, List<ProductDocumentDto>>
    {
        private readonly IProductDocumentRepository _repository;

        public GetProductDocumentsHandler(IProductDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDocumentDto>> Handle(GetProductDocumentCommand request, CancellationToken cancellationToken)
        {
            var documents = await _repository.GetByProductIdAsync(request.ProductId, cancellationToken);

            return documents.Select(d => new ProductDocumentDto
            {
                ProductId = d.ProductId!.Value,
                FileName = d.FileName,
                ContentType = d.ContentType,
                Data = d.Data
            }).ToList();
        }
    }
}
