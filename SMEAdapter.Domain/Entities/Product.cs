using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMEAdapter.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid? CompanyId { get; private set; }

        // Multilingual manufacturer name
        public LangStringSet ManufacturerName { get; private set; } = new(null);

        // Owned value objects
        public ProductInfo ProductInfo { get; private set; } = new();
        public AddressInfo AddressInfo { get; private set; } = new();

        public LangStringSet SerialNumber { get; private set; } = new(null);

        public byte[]? CompanyLogo { get; private set; }
        public string? ImageUrl { get; private set; }

        public ICollection<ProductDocument>? Documents { get; private set; }

        public ICollection<ProductDocumentAssignment> DocumentAssignments { get; private set; }
          = new List<ProductDocumentAssignment>();


        // Required by EF Core
        protected Product() { }

        public Product(
            LangStringSet manufacturerName,
            ProductInfo productInfo,
            AddressInfo addressInfo,
            LangStringSet serialNumber,
            
            string? imageUrl = null)
        {
            ManufacturerName = manufacturerName;
            ProductInfo = productInfo;
            AddressInfo = addressInfo;
            SerialNumber = serialNumber;
            ImageUrl = imageUrl;
            

        }
        public void ReplaceManufacturerName(LangStringSet v) => ManufacturerName = v ?? new(null);
        public void ReplaceSerialNumber(LangStringSet v) => SerialNumber = v ?? new(null);
        public void ReplaceAddress(AddressInfo v) => AddressInfo = v ?? new();
        public void ReplaceProductInfo(ProductInfo v) => ProductInfo = v ?? new();
        public void SetImageUrl(string? url) => ImageUrl = url;
        public void SetCompany(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class ProductInfo
    {
        public LangStringSet ProductDesignation { get; private set; } = new(null);
        public LangStringSet ProductRoot { get; private set; } = new(null);
        public LangStringSet ProductFamily { get; private set; } = new(null);
        public LangStringSet ProductType { get; private set; } = new(null);
        public LangStringSet OrderCode { get; private set; } = new(null);
        public LangStringSet ArticleNumber { get; private set; } = new(null);

        public ProductInfo() { }

        public ProductInfo(
            LangStringSet designation,
            LangStringSet root,
            LangStringSet family,
            LangStringSet type,
            LangStringSet orderCode,
            LangStringSet articleNumber)
        {
            ProductDesignation = designation;
            ProductRoot = root;
            ProductFamily = family;
            ProductType = type;
            OrderCode = orderCode;
            ArticleNumber = articleNumber;
        }
    }

    public class AddressInfo
    {
        public LangStringSet Street { get; private set; } = new(null);
        public LangStringSet ZipCode { get; private set; } = new(null);
        public LangStringSet City { get; private set; } = new(null);
        public LangStringSet Country { get; private set; } = new(null);

        public AddressInfo() { }

        public AddressInfo(LangStringSet street, LangStringSet zip, LangStringSet city, LangStringSet country)
        {
            Street = street;
            ZipCode = zip;
            City = city;
            Country = country;
        }
    }

}
