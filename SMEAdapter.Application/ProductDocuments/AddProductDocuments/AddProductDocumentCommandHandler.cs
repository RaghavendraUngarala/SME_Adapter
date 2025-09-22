using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMEAdapter.Application.ProductDocuments.AddProductDocuments
{
    public class AddProductDocumentHandler : IRequestHandler<AddProductDocumentCommand, Guid>
    {
        private readonly IProductDocumentRepository _repository;

        public AddProductDocumentHandler(IProductDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddProductDocumentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ProductDocument;

            var document = new ProductDocument
            {
                ProductId = dto.ProductId,
                FileName = dto.FileName,
                ContentType = dto.ContentType,
                Data = dto.Data ?? Array.Empty<byte>()
            };

            await _repository.AddAsync(document, cancellationToken);

            return document.Id;
        }
    }
}