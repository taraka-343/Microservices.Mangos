using mango.webPortal.Models;

namespace mango.webPortal.services.Iservices
{
    public interface IBaseService
    {
        Task<responceDto?> sendAsync(requestDto reqData);
    }
}
