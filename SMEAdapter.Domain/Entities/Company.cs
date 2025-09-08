using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? ManufacturerName { get; set; }
        public ProductInfo? ProductInfo { get; set; }
        public string? SerialNumber { get; set; }
        public AddressInfo? AddressInfo { get; set; }
        public byte[]? CompanyLogo { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
