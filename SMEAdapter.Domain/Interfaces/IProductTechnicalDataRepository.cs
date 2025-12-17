using SMEAdapter.Domain.Entities.ProductTechnicalDataTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Domain.Interfaces
{
    public interface IProductTechnicalDataRepository
    {
        Task<ProductTechnicalData> GetByIdAsync(Guid id);
        Task<ProductTechnicalData> GetByProductIdAsync(Guid productId);
        Task<ProductTechnicalData> GetByIdWithDetailsAsync(Guid id);
        Task AddAsync(ProductTechnicalData productTechnicalData);
        Task UpdateAsync(ProductTechnicalData productTechnicalData);
        Task DeleteAsync(Guid id);

        Task<ProductTechnicalData> GetByProductIdWithDetailsAsync(Guid productId);
        Task<IEnumerable<ProductTechnicalData>> GetByTemplateIdAsync(Guid templateId);
        Task<bool> ExistsForProductAsync(Guid productId);
        Task<int> CountByTemplateIdAsync(Guid templateId);
        Task DeleteByProductIdAsync(Guid productId);
        Task<IEnumerable<ProductTechnicalData>> GetAllAsync();
    }
}
