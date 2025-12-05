using SMEAdapter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company, CancellationToken ct = default);
        Task UpdateAsync(Company company, CancellationToken ct = default);
        Task<Company?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Company>> GetAllAsync(CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
