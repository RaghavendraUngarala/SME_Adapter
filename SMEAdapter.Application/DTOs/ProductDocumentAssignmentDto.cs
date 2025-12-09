using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs
{
    public class ProductDocumentAssignmentDto
    {
        public Guid ProductId { get; set; }
        public Guid ProductDocumentId { get; set; }

        // Minimal Product information for display
        public string ProductDesignation { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;

        // Minimal Document information for display
        public string DocumentTitle { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string OwnershipType { get; set; } = string.Empty;
    }
}
