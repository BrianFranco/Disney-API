using System;
using System.ComponentModel.DataAnnotations;

namespace Disney_API.DTOs
{
    public class AuthLoginDTO
    {
        [Required]
        [EmailAddress]
        [MinLength(5)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
