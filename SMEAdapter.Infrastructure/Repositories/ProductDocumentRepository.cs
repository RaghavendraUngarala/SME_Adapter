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

        public async Task<List<ProductDocument>> GetByProductIdAsync(Guid productId, CancellationToken ct = default)
        {
            return await _context.ProductDocuments
                .Where(d => d.ProductId == productId)
                .ToListAsync(ct);
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var document = await _context.ProductDocuments.FindAsync( id , cancellationToken);
            if (document != null)
            {
                _context.ProductDocuments.Remove(document);
            }
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<ProductDocument?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.ProductDocuments
                .FirstOrDefaultAsync(d => d.Id == id, ct);
        }
        public async Task UpdateAsync(ProductDocument document, CancellationToken cancellationToken)
        {
            _context.ProductDocuments.Update(document);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
