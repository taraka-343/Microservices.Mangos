using Newtonsoft.Json;

namespace mangos.services.ShoppingCartAPI.Models.Dto
{
    public class CartDto
    {
        [JsonProperty("CartHeader")]
        public CartHeaderDto CartHeader { get; set; }
        [JsonProperty("CartDetails")]
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}
