using mango.webPortal.Models;

namespace mango.webPortal.services.Iservices
{
    public interface ICoupanService
    {
        Task<responceDto?> getCoupanAsync(string coupanCode);
        Task<responceDto?> getCoupanByIdAsync(int coupanId);
        Task<responceDto?> getAllCoupanAsync();
        Task<responceDto?> createCoupanAsync(coupan newCoupan);
        Task<responceDto?> updateCoupanAsync(coupan newCoupan);
        Task<responceDto> deleteCoupanAsync(int coupanId);






    }
}
