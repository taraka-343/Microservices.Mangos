using System.ComponentModel.DataAnnotations;

namespace mangos.services.ProductAPI.Models.Dto
{
    public class productDto
    {
        public int productId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
