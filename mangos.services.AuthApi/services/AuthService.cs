using mangos.services.AuthApi.Data;
using mangos.services.AuthApi.Models;
using mangos.services.AuthApi.Models.dto;
using mangos.services.AuthApi.services.IServices;
using Microsoft.AspNetCore.Identity;

namespace mangos.services.AuthApi.services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext appDbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator
            )
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> assignRole(string email, string roleName)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;

        }

        public async Task<loginResponceDto> Login(loginRequestDto LoginRequestDto)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == LoginRequestDto.userName.ToLower());
            if (user != null)
            {
                bool isValidPwd = await _userManager.CheckPasswordAsync(user, LoginRequestDto.password);
                if (isValidPwd)
                {
                    //get all the roles for the user
                    var roles = await _userManager.GetRolesAsync(user);
                    //Token Generate
                    var tokenDetails=_jwtTokenGenerator.generateJwtToken(user, roles);
                    userDto uData = new()
                    {
                        Name = user.name,
                        Email=user.Email,
                        PhoneNumber=user.PhoneNumber,
                        ID=user.Id 
                    };
                    loginResponceDto responce = new()
                    {
                        userDetails = uData,
                        token = tokenDetails
                    };
                    return responce;

                }
            }
            
                return new loginResponceDto
                {
                    userDetails = null,
                    token = ""
                };
            
        }

        public async Task<string> Register(registrationRequestDto RegistrationRequestDto)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = RegistrationRequestDto.Email,
                Email = RegistrationRequestDto.Email,
                NormalizedEmail = RegistrationRequestDto.Email.ToUpper(),
                PhoneNumber = RegistrationRequestDto.PhoneNumber,
                name = RegistrationRequestDto.Name

            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser,RegistrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var data = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.Email == RegistrationRequestDto.Email);
                    userDto resultData = new()
                    {
                        Email=data.Email,
                        Name=data.name,
                        PhoneNumber=data.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
