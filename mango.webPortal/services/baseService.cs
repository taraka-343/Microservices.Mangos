using mango.webPortal.Models;
using mango.webPortal.services.Iservices;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using static mango.webPortal.Utilities.DT;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;


namespace mango.webPortal.services
{
    public class baseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public baseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<responceDto?> sendAsync(requestDto reqData)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("mangoApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //get token and pass to api
                string token = _tokenProvider.getToken();
                message.Headers.Add("Authorization", $"Bearer {token}");
                message.RequestUri = new Uri(reqData.url);
                string jsonData = JsonConvert.SerializeObject(reqData.data);
                if (reqData.data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(reqData.data), Encoding.UTF8, "application/json");

                }
                HttpResponseMessage? apiResponce = null;
                switch (reqData.apiType)
                {
                    case apiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case apiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case apiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponce = await client.SendAsync(message);
                switch (apiResponce.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { isSuceed = false, message = "Not Found" };

                    case HttpStatusCode.Forbidden:
                        return new() { isSuceed = false, message = "Access Denied" };

                    case HttpStatusCode.Unauthorized:
                        return new() { isSuceed = false, message = "UnAuthorized" };

                    case HttpStatusCode.InternalServerError:
                        return new() { isSuceed = false, message = "Internal Server Error" };

                    default:
                        var apiContent = await apiResponce.Content.ReadAsStringAsync();
                        var apiResponceDto = JsonConvert.DeserializeObject<responceDto>(apiContent);
                        return apiResponceDto;

                }
            }
            catch(Exception ex)
            {
                var dto = new responceDto
                {
                    message = ex.Message.ToString(),
                    isSuceed = false
                };
                return dto;
            }
            }
            
    }
}
