using mangos.services.ShoppingCartAPI.Models.Dto;
using mangos.services.ShoppingCartAPI.services.IServices;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace mangos.services.ShoppingCartAPI.services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<productDto>> getProducts()
        {
            var client = _clientFactory.CreateClient("Product");
            var responce = await client.GetAsync($"api/Product");
            var apicontent = await responce.Content.ReadAsStringAsync();
            var finalRes = JsonConvert.DeserializeObject<responceDto>(apicontent);
            if (finalRes.isSuceed)
            {
                IEnumerable<productDto> products = JsonConvert.DeserializeObject<IEnumerable<productDto>>(Convert.ToString(finalRes.result));
                return products;
            }
            return new List<productDto>();

        }
    }
}
