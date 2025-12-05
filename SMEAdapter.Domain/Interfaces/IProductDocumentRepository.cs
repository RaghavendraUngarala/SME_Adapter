using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;

namespace SMEAdapter.Domain.Interfaces
{
    public  interface IProductDocumentRepository
    {
        Task AddAsync(ProductDocument document, CancellationToken cancellationToken);

        Task<List<ProductDocument>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<ProductDocument?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task UpdateAsync(ProductDocument document, CancellationToken cancellationToken);
        Task<IReadOnlyList<ProductDocument>> GetAllSharedAsync(CancellationToken ct = default);
        Task<IReadOnlyList<ProductDocument>> GetSharedForProductAsync(Guid productId, CancellationToken ct = default);
        

        Task AddAssignmentAsync(Guid productId, Guid documentId, CancellationToken cancellationToken);
        Task RemoveAssignmentAsync(Guid productId, Guid documentId, CancellationToken cancellationToken);
        Task<bool> AssignmentExistsAsync(Guid productId, Guid documentId, CancellationToken cancellationToken);
        Task RemoveAllAssignmentsForDocumentAsync(Guid documentId, CancellationToken cancellationToken);

        Task AddSharedDocumentsForProductAsync(Guid productId, IEnumerable<Guid> documentIds, CancellationToken ct);

    }

}
