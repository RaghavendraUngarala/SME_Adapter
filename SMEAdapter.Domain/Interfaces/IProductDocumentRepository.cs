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



    }
}
