using mango.webPortal.Models;
using mango.webPortal.services.Iservices;
using mango.webPortal.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace mango.webPortal.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=DT.roleAdmin,Value=DT.roleAdmin},
                new SelectListItem{Text=DT.roleCustomer,Value=DT.roleCustomer}

            };
            ViewBag.roleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(registrationRequestDto data)
        {
            responceDto result = await _authService.registrationAsync(data);
            if (result.isSuceed)
            {
                if (string.IsNullOrEmpty(data.role))
                {
                    data.role = DT.roleCustomer;
                }
                responceDto roleResult=await _authService.assignRoleAsync(data);
                if (roleResult.isSuceed)
                {
                    TempData["success"] = "User Registration successfull!";
                    return RedirectToAction(nameof(Login));

                }
            }
            else
            {
                TempData["error"] = result.message;
            }
            return View(data);

        }
        [HttpPost]
        public async Task<IActionResult> Login(loginRequestDto data)
        {
            responceDto resDto = await _authService.loginAsync(data);
            if (resDto.isSuceed)
            {
                loginResponceDto LoginResponce = JsonConvert.DeserializeObject<loginResponceDto>(Convert.ToString(resDto.result));
                await SignInUser(LoginResponce);
                _tokenProvider.setToken(LoginResponce.token);
                TempData["success"] = "User LoggedIn successfull!";
                return RedirectToAction("Index","Home");
            }
            else 
            {
                TempData["error"] = resDto.message;
                return View(data);
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.clearToken();
            return RedirectToAction("Index","Home");
        }
        //this method is used to signin the user, tat user won't see login and register, only see logout
        private async Task SignInUser(loginResponceDto LoginResponce)
        {
            //extracting user details from token claims including roles
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(LoginResponce.token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role")?.Value??string.Empty));



            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }
}
