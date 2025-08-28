using mango.webPortal.Models;

namespace mango.webPortal.services.Iservices
{
    public interface IProductService
    {
        Task<responceDto?> getProductByIdAsync(int productId);
        Task<responceDto?> getAllProductAsync();
        Task<responceDto?> createProductAsync(productDto newProduct);
        Task<responceDto?> updateProductAsync(productDto newProduct);
        Task<responceDto> deleteProductAsync(int productId);






    }
}
