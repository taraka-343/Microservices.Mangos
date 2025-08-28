using System.ComponentModel.DataAnnotations;

namespace mangos.services.ProductAPI.Models
{
    public class product
    {
        [Key]
        public int productId { get; set; }
        [Required]
        public string name { get; set; }
        [Range(1,1000)]
        public double price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
