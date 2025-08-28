using mango.webPortal.Models;
using mango.webPortal.services.Iservices;
using mango.webPortal.Utilities;

namespace mango.webPortal.services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public Task<responceDto?> assignRoleAsync(registrationRequestDto RegistrationData)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.POST,
                url = DT.AuthApiBaseUrl + "/api/Auth/assignRole",
                data = RegistrationData
            });
            return data;
        }

        public Task<responceDto?> loginAsync(loginRequestDto LoginData)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.POST,
                url = DT.AuthApiBaseUrl + "/api/Auth/Login",
                data = LoginData
            });
            return data;
        }

        public Task<responceDto?> registrationAsync(registrationRequestDto RegistrationData)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.POST,
                url = DT.AuthApiBaseUrl + "/api/Auth/Register",
                data = RegistrationData
            });
            return data;
        }
    }
}
