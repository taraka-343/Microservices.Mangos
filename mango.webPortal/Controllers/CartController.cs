using mango.webPortal.Models;
using mango.webPortal.services.Iservices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace mango.webPortal.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await loadCartDtoBasedOnLoggedInUser());
        }
        public async Task<IActionResult> Remove(int cartDeailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            responceDto? responce = await _cartService.RemoveFromCartAsync(cartDeailsId);
            if (responce != null & responce.isSuceed)
            {
                TempData["success"] = "Details updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> applyCoupan(CartDto cartDTO)
        {
            responceDto? responce = await _cartService.ApplyCoupanAsync(cartDTO);
            if (responce != null & responce.isSuceed)
            {
                TempData["success"] = "Cart Applied successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> removeCoupan(CartDto cartDTO)
        {
            cartDTO.CartHeader.coupanCode = "";
            responceDto? responce = await _cartService.ApplyCoupanAsync(cartDTO);
            if (responce != null & responce.isSuceed)
            {
                TempData["success"] = "Cart Applied successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private async Task<CartDto> loadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            responceDto? responce = await _cartService.getCartByAsync(userId);
            if (responce!=null & responce.isSuceed)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responce.result));
                return cartDto;
            }
            return new CartDto();
        }
    }
}
