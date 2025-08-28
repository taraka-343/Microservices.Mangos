using mangos.services.ShoppingCartAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mangos.services.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        [Key]
        public int cartDetailsId { get; set; }
        public int cartHeaderId { get; set; }
        [ForeignKey("cartHeaderId")]
        public CartHeader cardHeader { get; set; }
        public int productId { get; set; }
        [NotMapped]
        public productDto Product { get; set; }
        public int count { get; set; }
    }
}
