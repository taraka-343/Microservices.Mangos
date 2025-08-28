using mango.webPortal.Models;

namespace mango.webPortal.services.Iservices
{
    public interface IAuthService
    {
        Task<responceDto?> registrationAsync(registrationRequestDto RegistrationData);
        Task<responceDto?> loginAsync(loginRequestDto LoginData);
        Task<responceDto?> assignRoleAsync(registrationRequestDto RegistrationData);


    }
}
