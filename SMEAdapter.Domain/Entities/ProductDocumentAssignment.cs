using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities
{
    public class ProductDocumentAssignment
    {
        public Guid ProductId { get; set; }
        public Guid ProductDocumentId { get; set; }

        public Product Product { get; private set; } = null!;
        public ProductDocument ProductDocument { get; private set; } = null!;

        private ProductDocumentAssignment() { } // EF

        public ProductDocumentAssignment(Guid productId, Guid documentId)
        {
            ProductId = productId;
            ProductDocumentId = documentId;
        }
    }
}
