using System.ComponentModel.DataAnnotations;

namespace PrivateLibrary.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
