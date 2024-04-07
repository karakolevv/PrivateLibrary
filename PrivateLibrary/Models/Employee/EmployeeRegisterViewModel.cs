using System.ComponentModel.DataAnnotations;

namespace PrivateLibrary.Models.Employee
{
    public class EmployeeRegisterViewModel
    {

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(50, ErrorMessage = "Собственото име не може да бъде по-дълго от 50 символа.")]
        [MinLength(1, ErrorMessage = "Собственото име трябва да бъде поне 1 символ.")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(50, ErrorMessage = "Бащиното име не може да бъде по-дълго от 50 символа.")]
        [MinLength(1, ErrorMessage = "Бащиното име трябва да бъде поне 1 символ.")]
        public string MiddleName { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(50, ErrorMessage = "Фамилията не може да бъде по-дълга от 50 символа.")]
        [MinLength(1, ErrorMessage = "Фамилията трябва да бъде поне 1 символ.")]
        public string LastName { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(50, ErrorMessage = "Потребителското име не може да бъде по-дълго от 50 символа.")]
        [MinLength(1, ErrorMessage = "Потребителското име трябва да бъде поне 1 символ.")]
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ЕГН-то трябва да бъде 10 цифри.")]
        public string EGN { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Номерът трябва да бъде 10 цифри.")]
        public string PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(256, ErrorMessage = "Имейлът не може да бъде по-дълъг от 256 символа.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Въведете валиден email адрес.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(256, ErrorMessage = "Паролата не може да бъде по-дълга от 256 символа.")]
        [MinLength(8, ErrorMessage = "Паролата трябва да бъде поне 8 символа.")]
        [DataType(DataType.Password, ErrorMessage = "Въведете валидна парола.")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Полето е задължително!")]
        [MaxLength(256, ErrorMessage = "Паролата не може да бъде по-дълга от 256 символа.")]
        [MinLength(8, ErrorMessage = "Паролата трябва да бъде поне 8 символа.")]
        [DataType(DataType.Password, ErrorMessage = "Въведете валидна парола.")]
        [Compare(nameof(Password), ErrorMessage = "Паролите не съвпадат.")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
