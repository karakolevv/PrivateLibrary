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
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Author { get; set; } = null!;

        [Required]
        public bool IsTaken { get; set; }

        [Required]
        public double CostPerDay { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$|^\d{13}$", ErrorMessage = "ISBN must be either 10 or 13 digits")]
        public string ISBN { get; set; } = null!;

        [ForeignKey(nameof(Reader))]
        public string? ReaderId { get; set; }

        public Reader? Reader { get; set; }
    }
}
