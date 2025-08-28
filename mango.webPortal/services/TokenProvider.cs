using mango.webPortal.services.Iservices;
using mango.webPortal.Utilities;
using Newtonsoft.Json.Linq;

namespace mango.webPortal.services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void clearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(DT.tokenCookie);
        }

        public string? getToken()
        {
            string? token=null;
            bool? isToken;
            isToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(DT.tokenCookie, out token);
            return isToken is true ? token : null;
        }

        public void setToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(DT.tokenCookie, token);
        }
    }
}
