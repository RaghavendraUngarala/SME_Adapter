using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Interfaces
{
    public interface ITechnicalDataTemplateRepository
    {
        Task<TechnicalDataTemplate> GetByIdAsync(Guid id);
        Task<IEnumerable<TechnicalDataTemplate>> GetAllAsync();
        Task<TechnicalDataTemplate> GetByIdWithDetailsAsync(Guid id); // Include sections & properties
        Task AddAsync(TechnicalDataTemplate template);
        Task UpdateAsync(TechnicalDataTemplate template);
        Task DeleteAsync(Guid id);

        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null);
        Task<bool> IsInUseAsync(Guid templateId);
        Task<int> GetUsageCountAsync(Guid templateId);
        Task<TechnicalDataTemplate> GetByNameAsync(string name);
        Task<IEnumerable<TechnicalDataTemplate>> GetAllWithSectionCountAsync();
        Task<IEnumerable<TechnicalDataTemplate>> SearchAsync(string searchTerm);
    }
}
