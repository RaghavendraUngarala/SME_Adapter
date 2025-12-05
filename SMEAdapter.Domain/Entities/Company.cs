using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Manufacturer Name is required")]
        public LangStringSet CompanyManufacturerName { get; private set; } = new(null);

        // stays nested
        public CompanyAddressInfo CompanyAddressInfo { get; private set; } = new();
        public string? CompanyImageUrl { get; private set; }

        private Company() { }
        public Company(LangStringSet name, CompanyAddressInfo address, string? imageUrl)
        {
            CompanyManufacturerName = name;
            CompanyAddressInfo = address;
            CompanyImageUrl = imageUrl;
        }
        public void ReplaceCompanyManufacturerName(LangStringSet value)
            => CompanyManufacturerName = value ?? new LangStringSet(null);

        public void ReplaceAddress(CompanyAddressInfo value)
            => CompanyAddressInfo = value ?? new CompanyAddressInfo();

        public void SetImageUrl(string? url)
            => CompanyImageUrl = url;

    }
    public class CompanyAddressInfo
    {
        public LangStringSet ZipCode { get; private set; } = new(null);
        public LangStringSet City { get; private set; } = new(null);
        public LangStringSet Country { get; private set; } = new(null);

        public CompanyAddressInfo() { }
        public CompanyAddressInfo(LangStringSet zip, LangStringSet city, LangStringSet country)
        {
            ZipCode = zip; City = city; Country = country;
        }

        public void Update(LangStringSet zip, LangStringSet city, LangStringSet country)
        {
            ZipCode = zip ?? new LangStringSet(null);
            City = city ?? new LangStringSet(null);
            Country = country ?? new LangStringSet(null);
        }
    }
}
