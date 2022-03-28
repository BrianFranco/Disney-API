using Disney_API.DTOs;
using Disney_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney_API.Controllers
{

    [Route("characters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CharactersController : ControllerBase
    {
        private readonly MyDBContext _context;
        public CharactersController(MyDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PersonajeListadoDTO>> Get([FromQuery] string name, [FromQuery] int age, [FromQuery] int idMovie)
        {
            try
            {
                var listPersonaje = _context.Personaje.ToList();
                if (name != null)
                {
                    listPersonaje = (from list in listPersonaje where list.Nombre.ToLower().Contains(name.ToLower()) select list).ToList();
                }
                if (age != 0)
                {
                    listPersonaje = (from list in listPersonaje where list.Edad == age select list).ToList();
                }
                if (listPersonaje.Count==0 || listPersonaje == null)
                {
                    return NotFound();
                }
                return Ok(from list in listPersonaje select new PersonajeListadoDTO { Imagen = list.Imagen, Nombre = list.Nombre });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("detalle/{id:int}")]
        public ActionResult<Personaje> DetallePersonaje(int id)
        {
            var PersonajeEncontrado = _context.Personaje.FirstOrDefault(x=>x.Id==id);
            if (PersonajeEncontrado == null)
            {
                return NotFound();
            }
            return PersonajeEncontrado;
        }
        [HttpPost]
        public async Task<ActionResult> Post(PersonajeCreateDTO newPersonaje)
        {
            try
            {
                var personaje = new Personaje
                {
                    Nombre = newPersonaje.Nombre,
                    Imagen = newPersonaje.Imagen,
                    Edad = newPersonaje.Edad,
                    Historia = newPersonaje.Historia,
                    Peso = newPersonaje.Peso,
                };
                personaje.Peliculas = _context.Pelicula.Where(x => x.Id == newPersonaje.PeliculaId).ToList();
                await _context.Personaje.AddAsync(personaje);
                await _context.SaveChangesAsync();
                return Ok("Se creo exitosamente el Personaje.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(PersonajeUpdateDTO Updatepersonaje)
        {
            try
            {
                Personaje personaje = new Personaje
                {
                    Id = Updatepersonaje.Id,
                    Edad = Updatepersonaje.Edad,
                    Historia = Updatepersonaje.Historia,
                    Imagen = Updatepersonaje.Imagen,
                    Nombre = Updatepersonaje.Nombre,
                    Peso = Updatepersonaje.Peso
                };
                personaje.Peliculas = _context.Pelicula.Where(x => x.Id == Updatepersonaje.PeliculaId).ToList();
                _context.Personaje.Update(personaje);
                await _context.SaveChangesAsync();
                return Ok("Se modifico exitosamente el Personaje.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var DeletePersonaje = _context.Personaje.FirstOrDefault(d => d.Id == id);
                if (DeletePersonaje != null)
                {
                    _context.Personaje.Remove(DeletePersonaje);
                    await _context.SaveChangesAsync();
                    return Ok("Se elimino el personaje.");
                }
                else
                {
                    return BadRequest("El personaje no fue encontrado o no existe.");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
