using MediatR;
using SMEAdapter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEAdapter.Application.ProductTechnicalDataTemplate.UpdatePropertyValue
{
    public class UpdatePropertyValueCommandHandler : IRequestHandler<UpdatePropertyValueCommand, Unit>
    {
        private readonly IProductTechnicalDataRepository _repository;

        public UpdatePropertyValueCommandHandler(IProductTechnicalDataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(
            UpdatePropertyValueCommand request,
            CancellationToken cancellationToken)
        {
            var productData = await _repository.GetByIdAsync(request.ProductTechnicalDataId);

            if (productData == null)
            {
                throw new InvalidOperationException(
                    $"Product technical data with ID '{request.ProductTechnicalDataId}' not found.");
            }

            // Update the value
            productData.SetPropertyValue(request.TemplatePropertyId, request.Value);

            await _repository.UpdateAsync(productData);

            return Unit.Value;
        }
    }
}
