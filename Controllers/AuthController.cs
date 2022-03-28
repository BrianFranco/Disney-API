using Disney_API.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Disney_API.Controllers
{
    
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthController(UserManager<IdentityUser> userManager,IConfiguration configuration,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthTokenDTO>> Login(AuthLoginDTO login)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return ConstruirToken(login);
            }
            else
            {
                return BadRequest("Usuario o Contraseña incorrecto.");
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthTokenDTO>> Register(AuthRegisterDTO user)
        {
            var usuario = new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email
            };
            var result = await userManager.CreateAsync(usuario,user.Password);

            if (result.Succeeded)
            {
                //EnvioEmail ex = new EnvioEmail(user.Email, user.Nombre);
                return ConstruirToken(new AuthLoginDTO
                {
                    Email=user.Email
                });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        private AuthTokenDTO ConstruirToken(AuthLoginDTO user)
        {
            var claims = new List<Claim>()
            {
                new Claim("email",user.Email),
                new Claim("nombreClaim", "ValorClaim")
            };
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llaveJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddDays(1);
            var securityToken = new JwtSecurityToken(issuer:null,audience:null,claims:claims,expires:expiracion,signingCredentials:creds);
            return new AuthTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }
    }
    internal class EnvioEmail
    {
        static string EmailDestino;
        static string NombreDestino;
        public EnvioEmail(string emailDestino, string nombreDestino)
        {
            EmailDestino = emailDestino;
            NombreDestino = nombreDestino;
            Execute().Wait();
        }
        static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY_API_DISNEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("brianfranco9466@gmail.com", "CHALLENGE BACKEND - C# .NET - API Disney");
            var subject = "REGISTRO DE USUARIO EN API DISNEY";
            var to = new EmailAddress(EmailDestino, NombreDestino);
            var plainTextContent = "¡BIENVENIDO! \n Su registro en la aplicacion API Disney fue exitoso.\n Saludos.";
            var htmlContent = "¡BIENVENIDO! \n Su registro en la aplicacion API Disney fue exitoso.\n Saludos.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
