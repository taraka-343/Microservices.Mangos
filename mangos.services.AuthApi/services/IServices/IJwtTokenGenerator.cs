using mangos.services.AuthApi.Models;
using mangos.services.AuthApi.Models.dto;

namespace mangos.services.AuthApi.services.IServices
{
    public interface IJwtTokenGenerator
    {
        string generateJwtToken(ApplicationUser applicationUser,IEnumerable<string> roles);  
    }
}
