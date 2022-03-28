using System;
using System.ComponentModel.DataAnnotations;


namespace Disney_API.DTOs
{
    public class AuthRegisterDTO
    {
        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        [MinLength(5)]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
