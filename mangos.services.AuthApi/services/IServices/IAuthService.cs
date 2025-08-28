using mangos.services.AuthApi.Models.dto;

namespace mangos.services.AuthApi.services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(registrationRequestDto RegistrationRequestDto);
        Task<loginResponceDto> Login(loginRequestDto LoginRequestDto);
        Task<bool> assignRole(string email, string roleName);
    }
}
