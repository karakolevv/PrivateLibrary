using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateLibrary.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(100, ErrorMessage = "Заглавието не трябва да надвишава 100 символа!")]
        [MinLength(1, ErrorMessage = "Заглавието не трябва да бъде поне 1 символ!")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(50, ErrorMessage = "Името на автора не трябва да надвишава 100 символа!")]
        [MinLength(1, ErrorMessage = "Името на автора трябва да бъде поне 1 символ!")]
        public string Author { get; set; } = default!;

        [Required]
        public bool IsTaken { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public double CostPerDay { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^\d{10}$|^\d{13}$", ErrorMessage = "ISBN трябва да е 10 или 13 цифри!")]
        public string ISBN { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^(http(s)?:\/\/)?([\w-]+\.)+[\w-]+(\/[\w- ;,./?%&=]*)?$", ErrorMessage = "Въведете валиден URL!")]
        public string Image { get; set; } = default!;
    }
}
