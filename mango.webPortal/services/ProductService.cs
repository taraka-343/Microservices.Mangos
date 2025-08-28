using mango.webPortal.Models;
using mango.webPortal.services.Iservices;
using mango.webPortal.Utilities;

namespace mango.webPortal.services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService sbaseService)
        {
            _baseService = sbaseService;
        }
        public Task<responceDto?> createProductAsync(productDto newProduct)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.POST,
                url = DT.productApiBaseUrl + "/api/Product/CreateProduct",
                data = newProduct
            });
            return data;
        }

        public async Task<responceDto> deleteProductAsync(int productId)
        {
            var data = await _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.DELETE,
                url = DT.productApiBaseUrl + "/api/Product/deleteProduct/" + productId
            });
            return data;
        }

        public async Task<responceDto?> getAllProductAsync()
        {
            var data = await _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.GET,
                url = DT.productApiBaseUrl + "/api/Product"
            });
            return data;
        }

        public Task<responceDto?> getProductByIdAsync(int productId)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.GET,
                url = DT.productApiBaseUrl + "/api/Product/" + productId
            });
            return data;
        }

        public Task<responceDto?> updateProductAsync(productDto newProduct)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.PUT,
                url = DT.productApiBaseUrl + "/api/Product",
                data = newProduct
            });
            return data;
        }
    }
}
