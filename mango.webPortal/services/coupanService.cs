using mango.webPortal.Models;
using mango.webPortal.services.Iservices;
using mango.webPortal.Utilities;

namespace mango.webPortal.services
{
    public class coupanService:ICoupanService
    {
        private readonly IBaseService _baseService;
        public coupanService(IBaseService sbaseService)
        {
            _baseService = sbaseService;
        }

        public Task<responceDto?> createCoupanAsync(coupan newCoupan)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.POST,
                url = DT.coupanApiBaseUrl + "/api/CoupanApi/CreateCoupan",
                data= newCoupan
            });
            return data;
        }

        public async Task<responceDto?> deleteCoupanAsync(int coupanId)
        {
            var data = await _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.DELETE,
                url = DT.coupanApiBaseUrl + "/api/CoupanApi/deleteCoupan/" + coupanId
            });
            return data;
            
        }

        public async Task<responceDto?> getAllCoupanAsync()
        {
           var data = await _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.GET,
                url = DT.coupanApiBaseUrl+ "/api/CoupanApi"
           });
            return data;
        }

        public Task<responceDto?> getCoupanAsync(string coupanCode)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.GET,
                url = DT.coupanApiBaseUrl + "/api/CoupanApi/getByCode" + coupanCode
            });
            return data;
        }

        public Task<responceDto?> getCoupanByIdAsync(int coupanId)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.GET,
                url = DT.coupanApiBaseUrl + "/api/CoupanApi/" + coupanId
            });
            return data;
        }

        public Task<responceDto?> updateCoupanAsync(coupan newCoupan)
        {
            var data = _baseService.sendAsync(new requestDto()
            {
                apiType = DT.apiType.PUT,
                url = DT.coupanApiBaseUrl + "/api/CoupanApi",
                data = newCoupan
            });
            return data;
        }
    }
}
