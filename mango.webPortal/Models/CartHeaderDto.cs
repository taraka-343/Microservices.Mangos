using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace mango.webPortal.Models
{
    public class CartHeaderDto
    {
        [JsonProperty("CartHeaderId")]
        public int cartHeaderId { get; set; }
        [JsonProperty("userId")]
        public string? userId { get; set; }
        [JsonProperty("coupanCode")]
        public string? coupanCode { get; set; }
        [JsonProperty("discount")]
        public double discount { get; set; }
        [JsonProperty("cartTotal")]
        public double cartTotal { get; set; }
    }
}
