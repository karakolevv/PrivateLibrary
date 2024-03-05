﻿using System.ComponentModel.DataAnnotations;

namespace PrivateLibrary.Models
{
    public class ReaderRegisterViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "First Name cannot be longer than 100 characters.")]
        [MinLength(1, ErrorMessage = "First Name must be at least 1 character.")]
        public string FirstName { get; set; } = default!;

        [Required]
        [MaxLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
        [MinLength(1, ErrorMessage = "Last Name must be at least 1 character.")]
        public string LastName { get; set; } = default!;

        [Required]
        [MaxLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
        [MinLength(1, ErrorMessage = "Last Name must be at least 1 character.")]
        public string UserName { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        [Required]
        [MaxLength(256, ErrorMessage = "Email cannot be longer than 256 characters.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(256, ErrorMessage = "Password cannot be longer than 256 characters.")]
        [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        [MaxLength(256, ErrorMessage = "Password cannot be longer than 256 characters.")]
        [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
