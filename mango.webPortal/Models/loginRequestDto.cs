using System.ComponentModel.DataAnnotations;

namespace mango.webPortal.Models
{
    public class loginRequestDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
