using MediatR;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Companies.CreateCompanies
{
    public class CreateCompanyCommandHandler
        : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly ICompanyRepository _repo;

        public CreateCompanyCommandHandler(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken ct)
        {
            var dto = request.Company;

            var company = new Company(
                name: LangStringSet.FromDictionary(dto.CompanyManufacturerName),
                address: new CompanyAddressInfo(
                    LangStringSet.FromDictionary(dto.CompanyAddressInfo.ZipCode),
                    LangStringSet.FromDictionary(dto.CompanyAddressInfo.City),
                    LangStringSet.FromDictionary(dto.CompanyAddressInfo.ZipCode)
                ),
                imageUrl: dto.CompanyImageUrl
            );

            await _repo.AddAsync(company, ct);

            return company.Id;
        }
    }
}
