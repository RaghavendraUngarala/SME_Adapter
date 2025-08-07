using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMEAdapter.Domain.Entities;

namespace SMEAdapter.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, CancellationToken ct = default);
        Task<List<Product>> GetAllAsync(CancellationToken ct = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(Product product);
    }
}
