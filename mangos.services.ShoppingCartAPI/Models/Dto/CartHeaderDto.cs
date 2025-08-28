using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mangos.services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public int cartHeaderId { get; set; }
        public string? userId { get; set; }
        public string? coupanCode { get; set; }
        public double discount { get; set; }
        public double cartTotal { get; set; }
    }
}
