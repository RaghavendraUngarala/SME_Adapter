using MediatR;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.AddCustomProperty
{
   
        public class AddCustomPropertyCommandHandler : IRequestHandler<AddCustomPropertyCommand, Unit>
        {
            private readonly IProductTechnicalDataRepository _repository;

            public AddCustomPropertyCommandHandler(IProductTechnicalDataRepository repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public async Task<Unit> Handle(
                AddCustomPropertyCommand request,
                CancellationToken cancellationToken)
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    throw new ArgumentException(
                        "Custom property name cannot be empty.",
                        nameof(request.Name));
                }

                if (string.IsNullOrWhiteSpace(request.Value))
                {
                    throw new ArgumentException(
                        "Custom property value cannot be empty.",
                        nameof(request.Value));
                }

                // Get product technical data
                var productData = await _repository.GetByIdAsync(request.ProductTechnicalDataId);

                if (productData == null)
                {
                    throw new InvalidOperationException(
                        $"Product technical data with ID '{request.ProductTechnicalDataId}' not found.");
                }

                // Add custom property
                productData.AddCustomProperty(request.Name, request.Value, request.SemanticId);

                // Save changes
                await _repository.UpdateAsync(productData);

                return Unit.Value;
            }
        }
    
}
