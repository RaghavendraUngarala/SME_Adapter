using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMEAdapter.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? ManufacturerName{ get; set; }
        public ProductInfo? ProductInfo { get; set; }
        public string? SerialNumber { get; set; }
        public AddressInfo? AddressInfo { get; set; }
        public byte[]? CompanyLogo { get; set; }

        public string? ImageUrl { get; set; }
    }

    public class ProductInfo
    {
        public string? ProductDesignation { get; set; }
        public string? ProductRoot { get; set; }
        public string? ProductFamily { get; set; }
        public string? ProductType { get; set; }
        public string? OrderCode { get; set; }
        public string ArticleNumber { get; set; }
    }


    public class AddressInfo
    {
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string Country { get; set; }
        
    }

  
}
