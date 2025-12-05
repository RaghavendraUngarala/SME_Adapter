using Microsoft.EntityFrameworkCore;
using SMEAdapter.Domain.Entities;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Infrastructure.Repositories
{
    public class ProductDocumentRepository : IProductDocumentRepository
    {
        private readonly DataContext _context;
        public ProductDocumentRepository(DataContext context) => _context = context;
        public async Task AddAsync(ProductDocument document, CancellationToken ct = default)
        {
            _context.ProductDocuments.Add(document);
            await _context.SaveChangesAsync(ct);
        }
        public async Task UpdateAsync(ProductDocument document, CancellationToken cancellationToken)
        {
            _context.ProductDocuments.Update(document);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            await RemoveAllAssignmentsForDocumentAsync(id, ct);

            var doc = await _context.ProductDocuments.FindAsync(id, ct);
            if (doc != null)
            {
                _context.ProductDocuments.Remove(doc);
                await _context.SaveChangesAsync(ct);
            }
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ProductDocument>> GetByProductIdAsync(Guid productId, CancellationToken ct = default)
        {
            return await _context.ProductDocuments
                .Where(d => d.ProductId == productId)
                .ToListAsync(ct);
        }
        
        public async Task<ProductDocument?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.ProductDocuments
                .FirstOrDefaultAsync(d => d.Id == id, ct);
        }
       

        public async Task<IReadOnlyList<ProductDocument>> GetAllSharedAsync(CancellationToken ct = default)
        {
            return await _context.ProductDocuments
                .Where(d => d.OwnershipType == ProductDocument.DocumentOwnershipType.Shared)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<ProductDocument>> GetSharedForProductAsync(Guid productId, CancellationToken ct = default)
        {
            return await _context.ProductDocumentAssignments
                .Where(a => a.ProductId == productId &&
                            a.ProductDocument.OwnershipType == ProductDocument.DocumentOwnershipType.Shared)
                .Select(a => a.ProductDocument)
                .ToListAsync(ct);
        }



        public async Task AddAssignmentAsync(Guid productId, Guid documentId, CancellationToken ct)
        {
            bool exists = await _context.ProductDocumentAssignments
                .AnyAsync(a => a.ProductId == productId && a.ProductDocumentId == documentId, ct);

            if (!exists)
            {
                await _context.ProductDocumentAssignments.AddAsync(
                    new ProductDocumentAssignment(productId, documentId), ct);

                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task RemoveAssignmentAsync(Guid productId, Guid documentId, CancellationToken ct)
        {
            var entity = await _context.ProductDocumentAssignments
                .FirstOrDefaultAsync(a => a.ProductId == productId && a.ProductDocumentId == documentId, ct);

            if (entity != null)
            {
                _context.ProductDocumentAssignments.Remove(entity);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<bool> AssignmentExistsAsync(Guid productId, Guid documentId, CancellationToken ct)
        {
            return await _context.ProductDocumentAssignments
                .AnyAsync(a => a.ProductId == productId && a.ProductDocumentId == documentId, ct);
        }

        public async Task RemoveAllAssignmentsForDocumentAsync(Guid documentId, CancellationToken ct)
        {
            var list = await _context.ProductDocumentAssignments
                .Where(a => a.ProductDocumentId == documentId)
                .ToListAsync(ct);

            if (list.Any())
            {
                _context.ProductDocumentAssignments.RemoveRange(list);
                await _context.SaveChangesAsync(ct);
            }
        }
        public async Task AddSharedDocumentsForProductAsync(Guid productId, IEnumerable<Guid> documentIds, CancellationToken ct)
        {
            foreach (var id in documentIds.Distinct())
            {
                if (!await AssignmentExistsAsync(productId, id, ct))
                {
                    _context.ProductDocumentAssignments.Add(new ProductDocumentAssignment(productId, id));
                }
            }

            await _context.SaveChangesAsync(ct);
        }
    }

}
