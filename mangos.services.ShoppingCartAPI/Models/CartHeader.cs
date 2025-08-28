using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mangos.services.ShoppingCartAPI.Models
{
    public class CartHeader
    {
        [Key]
        public int cartHeaderId { get; set; }
        public string? userId { get; set; }
        public string? coupanCode { get; set; }
        [NotMapped]
        public double discount { get; set; }
        [NotMapped]
        public double cartTotal { get; set; }
    }
}
