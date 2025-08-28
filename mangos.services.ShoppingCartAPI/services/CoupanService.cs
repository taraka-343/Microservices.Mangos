using mangos.services.ShoppingCartAPI.Models.Dto;
using mangos.services.ShoppingCartAPI.services.IServices;
using Newtonsoft.Json;

namespace mangos.services.ShoppingCartAPI.services
{
    public class CoupanService : ICoupanService
    {
        private readonly IHttpClientFactory _clientFactory;
        public CoupanService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<coupanDto> getCoupan(string coupanCode)
        {
            var client = _clientFactory.CreateClient("Coupan");
            var responce = await client.GetAsync($"api/CoupanAPI/getByCode/" + coupanCode);
            var apicontent = await responce.Content.ReadAsStringAsync();
            var finalRes = JsonConvert.DeserializeObject<responceDto>(apicontent);
            if (finalRes.isSuceed)
            {
                coupanDto coupan = JsonConvert.DeserializeObject<coupanDto>(Convert.ToString(finalRes.result));
                return coupan;
            }
            return new coupanDto();
        }
    }
}
