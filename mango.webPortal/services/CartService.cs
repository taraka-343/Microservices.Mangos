using mango.webPortal.Models;
using mango.webPortal.services.Iservices;
using mango.webPortal.Utilities;

namespace mango.webPortal.services
{
    public class CartService:ICartService
    {
        private readonly IBaseService _baseService;
        public CartService(IBaseService sbaseService)
        {
            _baseService = sbaseService;
        }

        public async Task<responceDto?> ApplyCoupanAsync(CartDto cartDto)
        {
            var data = await _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.POST,
                url = DT.shoppingCartApiBaseUrl + "/api/CartAPI/ApplyCoupan",
                data = cartDto
            });
            return data;
        }

        public async Task<responceDto?> CartUpsertAsync(CartDto cartItems)
        {
            var data = await _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.POST,
                url = DT.shoppingCartApiBaseUrl + "/api/CartAPI/Cartupsert",
                data = cartItems
            });
            return data;
        }


        public async Task<responceDto?> getCartByAsync(string userId)
        {
            var data = await _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.GET,
                url = DT.shoppingCartApiBaseUrl + "/api/CartAPI/GetCart/"+ userId,
            });
            return data;
        }

        public async Task<responceDto> RemoveFromCartAsync(int cartDetailsId)
        {
            var data = await _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.POST,
                url = DT.shoppingCartApiBaseUrl + "/api/CartAPI/CartDelete",
                data = cartDetailsId
            });
            return data;
        }

       
    }
}
