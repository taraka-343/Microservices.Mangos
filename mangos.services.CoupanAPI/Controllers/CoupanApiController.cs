using AutoMapper;
using mangos.services.CoupanAPI.Data;
using mangos.services.CoupanAPI.Models;
using mangos.services.CoupanAPI.Models.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace mangos.services.CoupanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoupanApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly responceDto _responce;
        private IMapper _mapper;
        public CoupanApiController(AppDbContext db,IMapper mapper)
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
                IEnumerable<coupan> coupanData = _db.Coupans.ToList();
                _responce.result = _mapper.Map<IEnumerable<coupanDto>>(coupanData);
                
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
                coupan coupanData = _db.Coupans.First(u => u.coupanId == id);
               _responce.result= _mapper.Map<coupanDto>(coupanData);
            }
            catch (Exception ex)
            {
                _responce.isSuceed = false;
                _responce.message = ex.Message;
            }
            return _responce;
        }
        [HttpGet]
        [Route("getByCode/{code}")]
        public responceDto get(string code)
        {
            try
            {
                coupan coupanData = _db.Coupans.First(u => u.coupanCode.ToLower() == code.ToLower());
                _responce.result = _mapper.Map<coupanDto>(coupanData);
            }
            catch (Exception ex)
            {
                _responce.isSuceed = false;
                _responce.message = ex.Message;
            }
            return _responce;
        }
        [HttpPost]
        [Route("CreateCoupan")]
        [Authorize(Roles = "Admin")]
        public responceDto post([FromBody] coupanDto codeDto)
        {
            try
            {
                coupan data = _mapper.Map<coupan>(codeDto);
                _db.Coupans.Add(data);
                _db.SaveChanges();
                _responce.result = _mapper.Map<coupanDto>(data);
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
        public responceDto update([FromBody] coupanDto codeDto)
        {
            try
            {
                coupan data = _mapper.Map<coupan>(codeDto);
                _db.Coupans.Update(data);
                _db.SaveChanges();
                _responce.result = _mapper.Map<coupanDto>(data);
            }
            catch (Exception ex)
            {
                _responce.isSuceed = false;
                _responce.message = ex.Message;
            }
            return _responce;
        }
        [HttpDelete]
        [Route("deleteCoupan/{id:int}")]
        [Authorize(Roles = "Admin")]
        public responceDto delete(int id)
          {
            try
            {
                coupan data = _db.Coupans.First(u => u.coupanId == id);
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
