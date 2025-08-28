using System.ComponentModel.DataAnnotations;

namespace mango.webPortal.Models
{
    public class productDto
    {
        public int productId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        [Range(1,100)]
        public int Count { get; set; } = 1;
    }
}
