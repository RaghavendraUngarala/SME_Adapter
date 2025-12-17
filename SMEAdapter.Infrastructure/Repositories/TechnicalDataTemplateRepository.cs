using Microsoft.EntityFrameworkCore;
using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Infrastructure.Repositories
{
    public class TechnicalDataTemplateRepository : ITechnicalDataTemplateRepository
    {
        private readonly DataContext _context;

        public TechnicalDataTemplateRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TechnicalDataTemplate> GetByIdAsync(Guid id)
        {
            return await _context.TechnicalDataTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TechnicalDataTemplate>> GetAllAsync()
        {
            return await _context.TechnicalDataTemplates
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<TechnicalDataTemplate> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.TechnicalDataTemplates
                .AsNoTracking()
                .Include(t => t.Sections.OrderBy(s => s.Order))
                    .ThenInclude(s => s.Properties.OrderBy(p => p.Order))
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TechnicalDataTemplate> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Template name cannot be empty", nameof(name));

            return await _context.TechnicalDataTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<TechnicalDataTemplate>> GetAllWithSectionCountAsync()
        {
            return await _context.TechnicalDataTemplates
                .AsNoTracking()
                .Include(t => t.Sections)
                    .ThenInclude(s => s.Properties)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<TechnicalDataTemplate>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.TechnicalDataTemplates
                .AsNoTracking()
                .Where(t => t.Name.ToLower().Contains(lowerSearchTerm)
                         || t.Description.ToLower().Contains(lowerSearchTerm))
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var query = _context.TechnicalDataTemplates
                .Where(t => t.Name.ToLower() == name.ToLower());

            if (excludeId.HasValue)
            {
                query = query.Where(t => t.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> IsInUseAsync(Guid templateId)
        {
            return await _context.ProductTechnicalData
                .AnyAsync(ptd => ptd.TemplateId == templateId);
        }

        public async Task<int> GetUsageCountAsync(Guid templateId)
        {
            return await _context.ProductTechnicalData
                .CountAsync(ptd => ptd.TemplateId == templateId);
        }

        public async Task AddAsync(TechnicalDataTemplate template)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            await _context.TechnicalDataTemplates.AddAsync(template);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TechnicalDataTemplate template)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            _context.TechnicalDataTemplates.Update(template);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            // Check if in use before deleting
            var isInUse = await IsInUseAsync(id);
            if (isInUse)
            {
                throw new InvalidOperationException(
                    "Cannot delete template because it is currently being used by one or more products.");
            }

            var template = await _context.TechnicalDataTemplates
                .FirstOrDefaultAsync(t => t.Id == id);

            if (template != null)
            {
                _context.TechnicalDataTemplates.Remove(template);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<(TechnicalDataTemplate Template, int UsageCount)>> GetTemplatesWithUsageAsync()
        {
            var templates = await _context.TechnicalDataTemplates
                .AsNoTracking()
                .ToListAsync();

            var result = new List<(TechnicalDataTemplate, int)>();

            foreach (var template in templates)
            {
                var usageCount = await GetUsageCountAsync(template.Id);
                result.Add((template, usageCount));
            }

            return result.OrderByDescending(x => x.Item2);
        }
    }
}
