using System;

namespace Disney_API.DTOs
{
    public class AuthTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
