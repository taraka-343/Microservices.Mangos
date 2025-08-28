using AutoMapper;
using Azure;
using mangos.services.ShoppingCartAPI.Data;
using mangos.services.ShoppingCartAPI.Migrations;
using mangos.services.ShoppingCartAPI.Models;
using mangos.services.ShoppingCartAPI.Models.Dto;
using mangos.services.ShoppingCartAPI.services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mangos.services.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly responceDto _responceDto;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICoupanService _coupanService;
        public CartAPIController(AppDbContext db, IMapper mapper,IProductService productService, ICoupanService coupanService)
        {
            _db = db;
            _mapper = mapper;
            _responceDto = new responceDto();
            _productService = productService;
            _coupanService = coupanService;
        }
        [HttpGet("GetCart/{UserId}")]
        public async Task<responceDto> GetCart(string UserId)
        {
            try
            {
                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_db.cartHeaders.First(u=>u.userId == UserId))
                };
                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_db.cartDeatails.Where(u => u.cartHeaderId == cart.CartHeader.cartHeaderId));
                //calling product service -- consuming in another api shopping cart api
                IEnumerable<productDto> products=await _productService.getProducts();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = products.FirstOrDefault(u => u.productId == item.productId);
                    cart.CartHeader.cartTotal += item.count * item.Product.price;
                }
                //apply coupan if any
                if (!string.IsNullOrEmpty(cart.CartHeader.coupanCode))
                {
                    coupanDto coupanItem = await _coupanService.getCoupan(cart.CartHeader.coupanCode);
                    if (coupanItem != null & cart.CartHeader.cartTotal > coupanItem.minAmount)
                    {
                        cart.CartHeader.discount = coupanItem.discountAmount;
                        cart.CartHeader.cartTotal -= coupanItem.discountAmount;
                    }
                }
                _responceDto.result = cart;
            }
            catch (Exception ex)
            {
                _responceDto.message = ex.Message.ToString();
                _responceDto.isSuceed = false;
            }
            return _responceDto;

        }
        [HttpPost("Cartupsert")]
        public async Task<responceDto> CartUpsert(CartDto cartItems)
        {
            try
            {
                var cartHeaderFromDb = await _db.cartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.userId == cartItems.CartHeader.userId);
                if (cartHeaderFromDb == null)
                {
                    //create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartItems.CartHeader);
                    _db.cartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartItems.CartDetails.First().cartHeaderId = cartHeader.cartHeaderId;
                    _db.cartDeatails.Add(_mapper.Map<CartDetails>(cartItems.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //if header is not null
                    //check if details has same product
                    var cartDetailsFromDb = await _db.cartDeatails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.productId == cartItems.CartDetails.First().productId &&
                        u.cartHeaderId == cartHeaderFromDb.cartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        //create cartdetails
                        cartItems.CartDetails.First().cartHeaderId = cartHeaderFromDb.cartHeaderId;
                        _db.cartDeatails.Add(_mapper.Map<CartDetails>(cartItems.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in cart details
                        cartItems.CartDetails.First().count += cartDetailsFromDb.count;
                        cartItems.CartDetails.First().cartHeaderId = cartDetailsFromDb.cartHeaderId;
                        cartItems.CartDetails.First().cartDetailsId = cartDetailsFromDb.cartDetailsId;
                        _db.cartDeatails.Update(_mapper.Map<CartDetails>(cartItems.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _responceDto.result = cartItems;
            }
            catch (Exception ex)
            {
                _responceDto.message = ex.Message.ToString();
                _responceDto.isSuceed = false;
            }
            return _responceDto;

        }
        [HttpPost("CartDelete")]
        public async Task<responceDto> CartDelete([FromBody]int cartDetailsId)
        {
            try
            {
                CartDetails cartDetailsItem = _db.cartDeatails.First(H => H.cartDetailsId == cartDetailsId);
                int cartItemsCount = _db.cartDeatails.Where(u => u.cartHeaderId == cartDetailsItem.cartHeaderId).Count();
                _db.cartDeatails.Remove(cartDetailsItem);
                if (cartItemsCount == 1)
                {
                    var cartHeaderToRemove = _db.cartHeaders.FirstOrDefault(u => u.cartHeaderId == cartDetailsItem.cartHeaderId);
                    _db.cartHeaders.Remove(cartHeaderToRemove);
                }
                _db.SaveChanges();

                _responceDto.isSuceed = true;
            }
            catch (Exception ex)
            {
                _responceDto.message = ex.Message.ToString();
                _responceDto.isSuceed = false;
            }
            return _responceDto;

        }
        [HttpPost("ApplyCoupan")] 
        public async Task<responceDto> ApplyCoupan([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeaderDb = await _db.cartHeaders.FirstAsync(u => u.userId == cartDto.CartHeader.userId);
                cartHeaderDb.coupanCode = cartDto.CartHeader.coupanCode;
                _db.cartHeaders.Update(cartHeaderDb);
                await _db.SaveChangesAsync();
                _responceDto.isSuceed = true;
            }
            catch (Exception ex)
            {
                _responceDto.message = ex.Message.ToString();
                _responceDto.isSuceed = false;
            }
            return _responceDto;

        }
        [HttpPost("RemoveCoupan")]
        public async Task<responceDto> removeCoupan([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeaderDb = await _db.cartHeaders.FirstAsync(u => u.userId == cartDto.CartHeader.userId);
                cartHeaderDb.coupanCode = "";
                _db.cartHeaders.Update(cartHeaderDb);
                await _db.SaveChangesAsync();
                _responceDto.isSuceed = true;
            }
            catch (Exception ex)
            {
                _responceDto.message = ex.Message.ToString();
                _responceDto.isSuceed = false;
            }
            return _responceDto;

        }
    }
}
