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
    public class ProductRepository : IProductRepository
    {
        
        
            private readonly DataContext _context;
            public ProductRepository(DataContext context) => _context = context;

            public async Task AddAsync(Product product, CancellationToken ct = default)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync(ct);
            }

            public async Task<List<Product>> GetAllAsync(CancellationToken ct = default)
            {
                return await _context.Products
                    .Include(p => p.AddressInfo)
                    .Include(p => p.ProductInfo)
                    .ToListAsync(ct);
            }
        

    }
}
