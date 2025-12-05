using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.Companies.DeleteCompanies
{
    public record DeleteCompanyCommand(Guid CompanyId) : IRequest<Unit>;
}
