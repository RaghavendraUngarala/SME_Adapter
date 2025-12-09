using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductDocuments.ProductDocumentAssignmentQueries.GetAllProductDocumentAssignments
{
    public class GetAllProductDocumentAssignmentsQueryHandler
       : IRequestHandler<GetAllProductDocumentAssignmentsQuery, IReadOnlyList<ProductDocumentAssignmentDto>>
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductDocumentRepository _documentRepo;

        public GetAllProductDocumentAssignmentsQueryHandler(
            IProductRepository productRepo,
            IProductDocumentRepository documentRepo)
        {
            _productRepo = productRepo;
            _documentRepo = documentRepo;
        }

        public async Task<IReadOnlyList<ProductDocumentAssignmentDto>> Handle(
            GetAllProductDocumentAssignmentsQuery request,
            CancellationToken cancellationToken)
        {
            var products = await _productRepo.GetAllAsync(cancellationToken);
            var assignments = new List<ProductDocumentAssignmentDto>();

            static string GetEn(SMEAdapter.Domain.ValueObjects.LangStringSet? lss) =>
                lss?.Items.FirstOrDefault(x => x.Language.Equals("en", StringComparison.OrdinalIgnoreCase))?.Text
                ?? lss?.Items.FirstOrDefault()?.Text
                ?? string.Empty;

            foreach (var product in products)
            {
                var documents = await _documentRepo.GetByProductIdAsync(product.Id, cancellationToken);
                var sharedDocuments = await _documentRepo.GetSharedForProductAsync(product.Id, cancellationToken);
                var allDocuments = documents.Concat(sharedDocuments).DistinctBy(d => d.Id);

                foreach (var doc in allDocuments)
                {
                    assignments.Add(new ProductDocumentAssignmentDto
                    {
                        ProductId = product.Id,
                        ProductDocumentId = doc.Id,
                        ProductDesignation = GetEn(product.ProductInfo.ProductDesignation),
                        ManufacturerName = GetEn(product.ManufacturerName),
                        DocumentTitle = GetEn(doc.Version?.Title),
                        FileName = doc.FileName,
                        OwnershipType = doc.OwnershipType.ToString()
                    });
                }
            }

            return assignments;
        }
    }
}
