using mango.webPortal.Models;
using mango.webPortal.services;
using mango.webPortal.services.Iservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace mango.webPortal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            List<productDto> list = new();
            responceDto? data = await _productService.getAllProductAsync();
            if (data != null && data.isSuceed == true)
            {
                list = JsonConvert.DeserializeObject<List<productDto>>(Convert.ToString(data.result));
            }
            return View(list);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        public async Task<IActionResult> ProductEdit(int productId)
        {
            responceDto? data = await _productService.getProductByIdAsync(productId);
            if (data != null && data.isSuceed)
            {
                productDto resultData = JsonConvert.DeserializeObject<productDto>(Convert.ToString(data.result));
                return View(resultData);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(productDto data)
        {
            if (ModelState.IsValid)
            {
                responceDto? output = await _productService.createProductAsync(data);
                if (output != null && output.isSuceed)
                {
                    //toastr notification
                    TempData["success"] = "Product Created successfully!";
                    //TempData["ToastrType"] = "success";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = "Product Not Created!";
                }
            }
            return View(data);
        }
        public async Task<IActionResult> ProductDelete(int productId)
        {
            responceDto? data = await _productService.getProductByIdAsync(productId);
            if (data != null && data.isSuceed)
            {
                productDto resultData = JsonConvert.DeserializeObject<productDto>(Convert.ToString(data.result));
                return View(resultData);
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> ProductDelete(productDto productData)
        {
            responceDto data = await _productService.deleteProductAsync(productData.productId);
            if (data != null && data.isSuceed)
            {
                TempData["success"] = "Product Deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = "Product not deleted! there is an error";
            }
            return View(productData);

        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(productDto productData)
        {
            responceDto productInfo = await _productService.updateProductAsync(productData);
            if (productInfo.isSuceed)
            {
                    TempData["success"] = "Product Updated successfully";
                    return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = "Product not Updated: there is an error";
            }
            return View(productData);

        }
    }
}
