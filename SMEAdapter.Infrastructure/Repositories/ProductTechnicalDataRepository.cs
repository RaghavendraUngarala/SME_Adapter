using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;
using SMEAdapter.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Infrastructure.Repositories
{
    public class ProductTechnicalDataRepository : IProductTechnicalDataRepository
    {
        private readonly DataContext _context;

        public ProductTechnicalDataRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProductTechnicalData> GetByIdAsync(Guid id)
        {
            return await _context.ProductTechnicalData
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductTechnicalData> GetByProductIdAsync(Guid productId)
        {
            return await _context.ProductTechnicalData
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<ProductTechnicalData> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.ProductTechnicalData
                .AsNoTracking()
                .Include(ptd => ptd.Product)
                .Include(ptd => ptd.Template)
                    .ThenInclude(template => template.Sections.OrderBy(s => s.Order))
                        .ThenInclude(section => section.Properties.OrderBy(p => p.Order))
                .Include(ptd => ptd.Properties)
                    .ThenInclude(property => property.TemplateProperty)
                        .ThenInclude(templateProp => templateProp.Section)
                .FirstOrDefaultAsync(ptd => ptd.Id == id);
        }

        public async Task<ProductTechnicalData> GetByProductIdWithDetailsAsync(Guid productId)
        {
            return await _context.ProductTechnicalData
                .AsNoTracking()
                .Include(ptd => ptd.Product)
                .Include(ptd => ptd.Template)
                    .ThenInclude(template => template.Sections.OrderBy(s => s.Order))
                        .ThenInclude(section => section.Properties.OrderBy(p => p.Order))
                .Include(ptd => ptd.Properties)
                    .ThenInclude(property => property.TemplateProperty)
                .FirstOrDefaultAsync(ptd => ptd.ProductId == productId);
        }

        public async Task<IEnumerable<ProductTechnicalData>> GetAllAsync()
        {
            return await _context.ProductTechnicalData
                .AsNoTracking()
                .Include(ptd => ptd.Product)
                .Include(ptd => ptd.Template)
                .OrderBy(ptd => ptd.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductTechnicalData>> GetByTemplateIdAsync(Guid templateId)
        {
            return await _context.ProductTechnicalData
                .AsNoTracking()
                .Include(ptd => ptd.Product)
                    .ThenInclude(p => p.ProductInfo) // ← Include ProductInfo for sorting
                .Where(ptd => ptd.TemplateId == templateId)
                .ToListAsync();
        }

        public async Task<bool> ExistsForProductAsync(Guid productId)
        {
            return await _context.ProductTechnicalData
                .AnyAsync(ptd => ptd.ProductId == productId);
        }

        public async Task<int> CountByTemplateIdAsync(Guid templateId)
        {
            return await _context.ProductTechnicalData
                .CountAsync(ptd => ptd.TemplateId == templateId);
        }

        public async Task AddAsync(ProductTechnicalData productTechnicalData)
        {
            if (productTechnicalData == null)
                throw new ArgumentNullException(nameof(productTechnicalData));

            await _context.ProductTechnicalData.AddAsync(productTechnicalData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductTechnicalData productTechnicalData)
        {
            if (productTechnicalData == null)
                throw new ArgumentNullException(nameof(productTechnicalData));

            _context.ProductTechnicalData.Update(productTechnicalData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var data = await _context.ProductTechnicalData
                .FirstOrDefaultAsync(ptd => ptd.Id == id);

            if (data != null)
            {
                _context.ProductTechnicalData.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByProductIdAsync(Guid productId)
        {
            var dataList = await _context.ProductTechnicalData
                .Where(ptd => ptd.ProductId == productId)
                .ToListAsync();

            if (dataList.Any())
            {
                _context.ProductTechnicalData.RemoveRange(dataList);
                await _context.SaveChangesAsync();
            }
        }

    }
}
