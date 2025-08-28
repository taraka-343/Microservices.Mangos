using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mangos.services.ShoppingCartAPI.Models.Dto
{
    public class CartDetailsDto
    {
        public int cartDetailsId { get; set; }
        public int cartHeaderId { get; set; }
        public CartHeaderDto? cardHeader { get; set; }
        public int productId { get; set; }
        public productDto? Product { get; set; }
        public int count { get; set; }
    }
}
