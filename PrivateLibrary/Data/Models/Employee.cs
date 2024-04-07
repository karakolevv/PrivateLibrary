using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateLibrary.Data.Models
{
    public class Employee
    {
        [Key]
        public string Id { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; } = default!;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ЕГН-то трябва да бъде 10 цифри!")]
        public string EGN { get; set; } = default!;

        public DateTime HireDate { get; set; }

        public bool IsAccountActive { get; set; }

        public DateTime? LeaveDate { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;
    }
}
