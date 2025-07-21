using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs
{
     public class ProductDto
     {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Manufacturer Name is required")]
        public string ManufacturerName { get; set; } = string.Empty;
        public AddressInfo AddressInfo { get; set; } = new AddressInfo();
        public ProductInfo ProductInfo { get; set; } = new ProductInfo();
        public string SerialNumber { get; set; } = string.Empty;

    }

    public class AddressInfo
    {

        public string ZipCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

    }

    public class ProductInfo
    {
        public string ProductDesignation { get; set; }
        public string ProductRoot { get; set; }
        public string ProductFamily { get; set; }
        public string ProductType { get; set; }
        public string OrderCode { get; set; }
        public string ArticleNumber { get; set; }
    }

}
