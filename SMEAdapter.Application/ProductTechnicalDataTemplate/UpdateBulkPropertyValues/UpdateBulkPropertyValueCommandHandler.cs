using MediatR;
using SMEAdapter.Application.ProductTechnicalDataTemplate.NewFolder;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.UpdateBulkPropertyValues
{
    public class UpdateBulkPropertyValuesCommandHandler
        : IRequestHandler<UpdateBulkPropertyValuesCommand, Unit>
    {
        private readonly IProductTechnicalDataRepository _repository;

        public UpdateBulkPropertyValuesCommandHandler(IProductTechnicalDataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(
            UpdateBulkPropertyValuesCommand request,
            CancellationToken cancellationToken)
        {
            var productData = await _repository.GetByIdAsync(request.ProductTechnicalDataId);

            if (productData == null)
            {
                throw new InvalidOperationException(
                    $"Product technical data with ID '{request.ProductTechnicalDataId}' not found.");
            }

            // Update all property values
            foreach (var propertyValue in request.PropertyValues)
            {
                if (!string.IsNullOrWhiteSpace(propertyValue.Value))
                {
                    productData.SetPropertyValue(propertyValue.Key, propertyValue.Value);
                }
            }

            await _repository.UpdateAsync(productData);

            return Unit.Value;
        }
    }
}
