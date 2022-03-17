using Disney_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney_API.Controllers
{
    [Route("auth/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyDBContext _context;
        public LoginController(MyDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public string Post(string email, string contraseña)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var UserLogin = _context.Usuario.First(u => u.Email == email && u.Contraseña == contraseña);
                    if (UserLogin != null)
                    {
                        UserLogin.Token = Guid.NewGuid().ToString();
                        _context.Update(UserLogin);
                        _context.SaveChanges();
                        return "token:" + UserLogin.Token;
                    }
                    else
                    {
                        return "Email o Contraseña incorrecto.";
                    }
                }
                else
                {
                    return "Ingrese valores validos.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
    [Route("auth/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly MyDBContext _context;
        public RegisterController(MyDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public string Post(Usuario user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    _context.SaveChanges();
                    return "Usuario guardado correctamente.";
                }
                else
                {
                    return "Ingrese valores validos.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
