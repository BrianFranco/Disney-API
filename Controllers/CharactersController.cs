using Disney_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Disney_API.Controllers
{

    public class ViewModelPersonaje
    {
        public string Nombre;
        public string Imagen;
    }

    [Route("characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly MyDBContext _context;
        public CharactersController(MyDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public string Get([FromQuery] string name,[FromQuery]int age,[FromQuery]int idMovie)
        {
            var cont = _context.Personaje.ToList(); 
            string json = JsonSerializer.Serialize(from j in cont select new { nombre= j.Nombre,imagen = j.Imagen});
            return json;
         }
        [HttpGet("detalle/{id:int}")]
        public ActionResult<Personaje> DetallePersonaje(int id)
        {
            var PersonajeEncontrado = _context.Personaje.FirstOrDefault(x => x.Id == id);
            if (PersonajeEncontrado == null)
            {
                return NotFound();
            }
            return PersonajeEncontrado;
        }
        [HttpPost]
        public string Post(Personaje personaje)
        {
            try
            {
                _context.Personaje.Add(personaje);
                _context.SaveChanges();
                return "Se creo exitosamente el Personaje.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPut]
        public string Put(Personaje personaje)
        {
            try
            {
                _context.Personaje.Update(personaje);
                _context.SaveChanges();
                return "Se modifico exitosamente el Personaje.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpDelete]
        public string Delete(int id)
        {
            try
            {
                var DeletePersonaje = _context.Personaje.First(d => d.Id == id);
                if (DeletePersonaje!= null)
                {
                    _context.Personaje.Remove(DeletePersonaje);
                    _context.SaveChanges();
                    return "Se elimino el personaje.";
                }
                else
                {
                    return "El personaje no fue encontrado o no existe.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
