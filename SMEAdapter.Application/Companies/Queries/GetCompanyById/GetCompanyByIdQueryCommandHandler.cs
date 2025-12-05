using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQueryHandler
        : IRequestHandler<GetCompanyByIdQuery, CompanyDto>
    {
        private readonly ICompanyRepository _repo;

        public GetCompanyByIdQueryHandler(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<CompanyDto> Handle(GetCompanyByIdQuery request, CancellationToken ct)
        {
            var company = await _repo.GetByIdAsync(request.CompanyId, ct);

            if (company is null)
                throw new KeyNotFoundException($"Company with id '{request.CompanyId}' not found.");

            static Dictionary<string, string> ToDict(LangStringSet lss) =>
                lss.Items.ToDictionary(x => x.Language, x => x.Text, StringComparer.OrdinalIgnoreCase);

            return new CompanyDto
            {
                Id = company.Id,
                CompanyManufacturerName = ToDict(company.CompanyManufacturerName),
                CompanyImageUrl = company.CompanyImageUrl,
                CompanyAddressInfo = new CompanyAddressInfoDto
                {
                    ZipCode = ToDict(company.CompanyAddressInfo.ZipCode),
                    City = ToDict(company.CompanyAddressInfo.City),
                    Country = ToDict(company.CompanyAddressInfo.Country)
                }
            };
        }
    }
}
