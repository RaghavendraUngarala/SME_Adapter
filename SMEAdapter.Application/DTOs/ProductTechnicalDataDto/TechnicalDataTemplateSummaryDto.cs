using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs.ProductTechnicalDataDto
{
    public class TechnicalDataTemplateSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Statistics
        public int SectionCount { get; set; }
        public int PropertyCount { get; set; }
        public int UsageCount { get; set; } // How many products use this template
    }
}
