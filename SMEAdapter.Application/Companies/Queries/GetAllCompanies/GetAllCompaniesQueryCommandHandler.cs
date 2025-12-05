using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Companies.Queries.GetAllCompanies
{
    public class GetAllCompaniesQueryHandler
        : IRequestHandler<GetAllCompaniesQuery, List<CompanyDto>>
    {
        private readonly ICompanyRepository _repo;

        public GetAllCompaniesQueryHandler(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken ct)
        {
            var companies = await _repo.GetAllAsync(ct);

            static Dictionary<string, string> ToDict(LangStringSet lss) =>
                lss.Items.ToDictionary(x => x.Language, x => x.Text, StringComparer.OrdinalIgnoreCase);

            return companies.Select(c => new CompanyDto
            {
                Id = c.Id,
                CompanyManufacturerName = ToDict(c.CompanyManufacturerName),
                CompanyImageUrl = c.CompanyImageUrl,
                CompanyAddressInfo = new CompanyAddressInfoDto
                {
                    ZipCode = ToDict(c.CompanyAddressInfo.ZipCode),
                    City = ToDict(c.CompanyAddressInfo.City),
                    Country = ToDict(c.CompanyAddressInfo.Country)
                }
            }).ToList();
        }
    }
}
