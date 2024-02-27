using System.ComponentModel.DataAnnotations;

namespace PrivateLibrary.Data.Models
{
    public class TakenBook
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Book Book { get; set; } = null!;

        public Reader Reader { get; set; } = null!;

        public DateTime DateOfTaking { get; set; }

        public DateTime DateOfReturn { get; set; }

        public double Price { get; set; }
    }
}
