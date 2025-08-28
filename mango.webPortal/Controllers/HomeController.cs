using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mango.webPortal.Models;
using Newtonsoft.Json;
using mango.webPortal.services.Iservices;
using Microsoft.AspNetCore.Authorization;
using mango.webPortal.services;
//using IdentityModel;

namespace mango.webPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }
    public async Task<IActionResult> Index()
    {
        List<productDto> list = new();
        responceDto? data = await _productService.getAllProductAsync();
        if (data != null && data.isSuceed == true)
        {
            list = JsonConvert.DeserializeObject<List<productDto>>(Convert.ToString(data.result));
        }
        return View(list);
    }
    [Authorize]
    public async Task<IActionResult> ProductDetails(int productId)
    {
        productDto? list = new();
        responceDto? data = await _productService.getProductByIdAsync(productId);
        if (data != null && data.isSuceed == true)
        {
            list = JsonConvert.DeserializeObject<productDto>(Convert.ToString(data.result));
        }
        return View(list);
    }
    [Authorize]
    [HttpPost]
    [ActionName("ProductDetails")]
    public async Task<IActionResult> ProductDetails(productDto productDto)
    {
        CartDto cartDto = new CartDto()
        {
            CartHeader = new CartHeaderDto
            {
                userId = User.Claims.FirstOrDefault(u => u.Type == "sub")?.Value
              // userId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
            }
        };

        CartDetailsDto cartDetails = new CartDetailsDto()
        {
            count = productDto.Count,
            productId = productDto.productId,
        };

        List<CartDetailsDto> cartDetailsDtos = new() { cartDetails };
        cartDto.CartDetails = cartDetailsDtos;

        responceDto? response = await _cartService.CartUpsertAsync(cartDto);

        if (response != null && response.isSuceed)
        {
            TempData["success"] = "Item has been added to the Shopping Cart";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = response?.message;
        }

        return View(productDto);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
