using System.ComponentModel.DataAnnotations;

namespace mango.webPortal.Models
{
    public class registrationRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string? role { get; set; }
    }
}
