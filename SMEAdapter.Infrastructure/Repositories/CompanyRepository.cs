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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Company company, CancellationToken ct = default)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Company company, CancellationToken ct = default)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Company?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<List<Company>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Companies
                .ToListAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id, ct);

            if (company is null)
                return;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync(ct);
        }
    }
}
