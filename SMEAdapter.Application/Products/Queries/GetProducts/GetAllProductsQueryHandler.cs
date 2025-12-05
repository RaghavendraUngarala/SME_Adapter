using MediatR;
using SMEAdapter.Application.DTOs;
using SMEAdapter.Domain.Interfaces;



namespace SMEAdapter.Application.Products.Queries.GetProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductDocumentRepository _documentRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository , IProductDocumentRepository documentRepository)
        {
            _productRepository = productRepository;
            _documentRepository = documentRepository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            

            static Dictionary<string, string> ToDict(SMEAdapter.Domain.ValueObjects.LangStringSet lss) =>
                lss.Items.ToDictionary(x => x.Language, x => x.Text, StringComparer.OrdinalIgnoreCase);

            var dtos = new List<ProductDto>();

            foreach (var p in products)
            {
                // 🔹 Get document counts
                var owned = await _documentRepository.GetByProductIdAsync(p.Id, cancellationToken);
                var shared = await _documentRepository.GetSharedForProductAsync(p.Id, cancellationToken);

                var dto = new ProductDto
                {
                    Id = p.Id,
                    ManufacturerName = ToDict(p.ManufacturerName),
                    SerialNumber = ToDict(p.SerialNumber),
                    ImageUrl = p.ImageUrl,
                    CompanyId = p.CompanyId,
                    ProductInfo = new ProductInfoDto
                    {
                        ProductDesignation = ToDict(p.ProductInfo.ProductDesignation),
                        ProductRoot = ToDict(p.ProductInfo.ProductRoot),
                        ProductFamily = ToDict(p.ProductInfo.ProductFamily),
                        ProductType = ToDict(p.ProductInfo.ProductType),
                        OrderCode = ToDict(p.ProductInfo.OrderCode),
                        ArticleNumber = ToDict(p.ProductInfo.ArticleNumber)
                    },

                    AddressInfo = new AddressInfoDto
                    {
                        Street = ToDict(p.AddressInfo.Street),
                        ZipCode = ToDict(p.AddressInfo.ZipCode),
                        City = ToDict(p.AddressInfo.City),
                        Country = ToDict(p.AddressInfo.Country)
                    },

                    // ✅ ADD THESE
                    OwnedDocumentCount = owned.Count,
                    SharedDocumentCount = shared.Count,
                    TotalDocumentCount = owned.Count + shared.Count
                };

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
