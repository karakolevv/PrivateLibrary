using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PrivateLibrary.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = default!;
    }
}
