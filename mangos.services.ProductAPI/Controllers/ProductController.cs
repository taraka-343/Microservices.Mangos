using AutoMapper;
using mangos.services.ProductAPI.Data;
using mangos.services.ProductAPI.Models;
using mangos.services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mangos.services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly responceDto _responce;
        private IMapper _mapper;
        public ProductController(AppDbContext db,
            IMapper mapper)
        {
            _db = db;
            _responce = new responceDto();
            _mapper = mapper;
        }
        [HttpGet]
        public responceDto get()
        {
            try
            {
                IEnumerable<product> productData = _db.products.ToList();
                _responce.result = _mapper.Map<IEnumerable<productDto>>(productData);

            }
            catch (Exception ex)
            {
                _responce.isSuceed = false;
                _responce.message = ex.Message;

            }
            return _responce;
        }
        [HttpGet]
        [Route("{id:int}")]
        public responceDto get(int id)
        {
            try
            {
                product productData = _db.products.First(u => u.productId == id);
                _responce.result = _mapper.Map<productDto>(productData);
            }
            catch (Exception ex)
            {
                _responce.isSuceed = false;
                _responce.message = ex.Message;
            }
            return _responce;
        }
        [HttpPost]
        [Route("CreateProduct")]
        [Authorize(Roles = "Admin")]
        public responceDto post([FromBody] productDto dataDto)
        {
            try
            {
                product data = _mapper.Map<product>(dataDto);
                _db.products.Add(data);
                _db.SaveChanges();
                _responce.result = _mapper.Map<productDto>(data);
            }
            catch (Exception ex)
            {
                _responce.isSuceed = false;
                _responce.message = ex.Message;
            }
            return _responce;
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public responceDto update([FromBody] productDto codeDto)
        {
            try
            {
                product data = _mapper.Map<product>(codeDto);
                _db.products.Update(data);
                _db.SaveChanges();
                _responce.result = _mapper.Map<productDto>(data);
            }
            catch (Exception ex)
            {
                _responce.isSuceed = false;
                _responce.message = ex.Message;
            }
            return _responce;
        }
        [HttpDelete]
        [Route("deleteProduct/{id:int}")]
        [Authorize(Roles = "Admin")]
        public responceDto delete(int id)
        {
            try
            {
                product data = _db.products.First(u => u.productId == id);
                _db.Remove(data);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _responce.isSuceed = false;
                _responce.message = ex.Message;
            }
            return _responce;
        }
    }
}
