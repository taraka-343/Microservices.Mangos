using mangos.services.ShoppingCartAPI.Models.Dto;

namespace mangos.services.ShoppingCartAPI.services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<productDto>> getProducts();
    }
}
