using MediatR;
using SMEAdapter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Companies.Queries.GetCompanyById
{
    public record GetCompanyByIdQuery(Guid CompanyId) : IRequest<CompanyDto>;
}
