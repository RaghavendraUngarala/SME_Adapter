using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.DTOs
{
    public sealed class ProductDto
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> ManufacturerName { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> SerialNumber { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public int OwnedDocumentCount { get; set; }
        public int SharedDocumentCount { get; set; }
        public int TotalDocumentCount { get; set; }
        public ProductInfoDto ProductInfo { get; set; } = new();
        public AddressInfoDto AddressInfo { get; set; } = new();
        public Guid? CompanyId { get; set; }

        public string? ImageUrl { get; set; }
    }

    public sealed class ProductInfoDto
    {
        public Dictionary<string, string> ProductDesignation { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> ProductRoot { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> ProductFamily { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> ProductType { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> OrderCode { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> ArticleNumber { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    }

    public sealed class AddressInfoDto
    {
        public Dictionary<string, string> Street { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> ZipCode { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> City { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> Country{ get; set; } = new(StringComparer.OrdinalIgnoreCase);
    }

}
