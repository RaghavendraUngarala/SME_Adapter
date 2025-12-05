using MediatR;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using SMEAdapter.Domain.ValueObjects;

namespace SMEAdapter.Application.Companies.DeleteCompanies
{


    public class DeleteCompanyCommandHandler: IRequestHandler<DeleteCompanyCommand, Unit>
        {
            private readonly ICompanyRepository _repo;

            public DeleteCompanyCommandHandler(ICompanyRepository repo)
            {
                _repo = repo;
            }

            public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken ct)
            {
                await _repo.DeleteAsync(request.CompanyId, ct);
                return Unit.Value;
            }


        }
}
