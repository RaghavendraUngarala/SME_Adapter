using MediatR;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Companies.UpdateCompany
{
    public class UpdateCompanyCommandHandler
        : IRequestHandler<UpdateCompanyCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken ct)
        {
            var dto = request.Company;

            if (dto == null)
                throw new ArgumentNullException(nameof(request.Company));

            // 1. Load existing company
            var company = await _companyRepository.GetByIdAsync(dto.Id, ct);
            if (company is null)
                throw new KeyNotFoundException($"Company with id '{dto.Id}' was not found.");

            // 2. Map manufacturer name
            var nameLss = LangStringSet.FromDictionary(dto.CompanyManufacturerName);
            company.ReplaceCompanyManufacturerName(nameLss);

            // 3. Map address
            var address = new CompanyAddressInfo(
                zip: LangStringSet.FromDictionary(dto.CompanyAddressInfo.ZipCode),
                city: LangStringSet.FromDictionary(dto.CompanyAddressInfo.City),
                country: LangStringSet.FromDictionary(dto.CompanyAddressInfo.Country)
            );
            company.ReplaceAddress(address);

            // 4. Image URL
            company.SetImageUrl(dto.CompanyImageUrl);

            // 5. Persist
            await _companyRepository.UpdateAsync(company, ct);

            return Unit.Value;
        }
    }
}
