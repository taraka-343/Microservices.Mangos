using mangos.services.AuthApi.Models.dto;
using mangos.services.AuthApi.services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mangos.services.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public responceDto _responceDto;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _responceDto = new();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> register([FromBody] registrationRequestDto requestData)
        {
            string result = await _authService.Register(requestData);
            if (!string.IsNullOrEmpty(result))
            {
                _responceDto.isSuceed = false;
                _responceDto.message = result;
                return BadRequest(_responceDto);
            }
            return Ok(_responceDto);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody] loginRequestDto requestData)
        {
            var data = await _authService.Login(requestData);
            if (data.userDetails == null)
            {
                _responceDto.isSuceed = false;
                _responceDto.message = "UserName or Password Incorrect!";
                return BadRequest(_responceDto);
            }
            else
            {
                _responceDto.result = data;
                return Ok(_responceDto);

            }
        }
        [HttpPost("assignRole")]
        public async Task<IActionResult> assignRole([FromBody] registrationRequestDto requestData)
        {
            bool roleAssigned = await _authService.assignRole(requestData.Email,requestData.role.ToLower());
            if (!roleAssigned)
            {
                _responceDto.isSuceed = false;
                _responceDto.message = "Error Enciuntered!";
                return BadRequest(_responceDto);
            }
            return Ok(_responceDto);
        }
    }
}
