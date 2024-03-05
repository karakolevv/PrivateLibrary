using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateLibrary.Data.Models
{
    public class TakenBook
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Title { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Author { get; set; } = default!;

        public DateTime DateOfTaking { get; set; }

        public DateTime DateOfReturn { get; set; }

        [Required]
        public double Price { get; set; }

        [ForeignKey(nameof(Reader))]
        public string ReaderId { get; set; } = default!;

        public ApplicationUser? Reader { get; set; } = default!;
    }
}
