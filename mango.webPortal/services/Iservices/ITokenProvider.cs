namespace mango.webPortal.services.Iservices
{
    public interface ITokenProvider
    {
        void setToken(string token);
        string? getToken();
        void clearToken();
    }
}
