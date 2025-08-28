using mangos.services.ShoppingCartAPI.Models.Dto;

namespace mangos.services.ShoppingCartAPI.services.IServices
{
    public interface ICoupanService
    {
        Task<coupanDto> getCoupan(string coupanCode);
    }
}
