using mango.webPortal.Models;

namespace mango.webPortal.services.Iservices
{
    public interface ICartService
    {
        Task<responceDto?> getCartByAsync(string userId);
        Task<responceDto?> CartUpsertAsync(CartDto cartItems);
        Task<responceDto?> ApplyCoupanAsync(CartDto cartDto);
        Task<responceDto> RemoveFromCartAsync(int cartDetailsId);

    }
}
