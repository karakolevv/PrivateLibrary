using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PrivateLibrary.Data.Models
{
    public class Employee : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "EGN must be exactly 10 digits")]
        public string EGN { get; set; } = null!;

        public DateTime HireDate { get; set; }

        public bool IsAccountActive { get; set; }

        public DateTime? LeaveDate { get; set; }
    }
}
