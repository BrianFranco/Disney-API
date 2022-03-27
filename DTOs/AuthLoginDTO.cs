using System;
using System.ComponentModel.DataAnnotations;

namespace Disney_API.DTOs
{
    public class AuthLoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
