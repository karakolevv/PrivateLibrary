using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateLibrary.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Title { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Author { get; set; } = default!;

        [Required]
        public bool IsTaken { get; set; }

        [Required]
        public double CostPerDay { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$|^\d{13}$", ErrorMessage = "ISBN must be either 10 or 13 digits")]
        public string ISBN { get; set; } = default!;

        public string Image { get; set; } = default!;
    }
}
