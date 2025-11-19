using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Manufacturer Name in multiple languages
        [Required(ErrorMessage = "Manufacturer Name is required")]
        public Dictionary<string, string>? CompanyManufacturerName { get; set; }
            = new(StringComparer.OrdinalIgnoreCase);

        public CompanyAddressInfoDto CompanyAddressInfo { get; set; }
            = new CompanyAddressInfoDto();

        public string? CompanyImageUrl { get; set; }
    }

    public class CompanyAddressInfoDto
    {
        // All multilingual
        public Dictionary<string, string> ZipCode { get; set; }
            = new(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string, string> City { get; set; }
            = new(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string, string> Country { get; set; }
            = new(StringComparer.OrdinalIgnoreCase);
    }
}
