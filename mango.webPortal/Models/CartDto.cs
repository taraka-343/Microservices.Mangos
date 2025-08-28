using Newtonsoft.Json;

namespace mango.webPortal.Models
{
    public class CartDto
    {
        [JsonProperty("CartHeader")]
        public CartHeaderDto CartHeader { get; set; }
        [JsonProperty("CartDetails")]
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
