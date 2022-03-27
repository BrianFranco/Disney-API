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

    public class ViewModelPersonaje
    {
        public string Nombre;
        public string Imagen;
    }

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
        public ActionResult<List<PersonajeListadoDTO>> Get([FromQuery] string name, [FromQuery] int age, [FromQuery] int idMovie)
        {
            try
            {
                var cont = _context.Personaje.ToList();
                List<PersonajeListadoDTO> listPersonaje = new List<PersonajeListadoDTO>();
                if (name == null && age == 0)
                {
                    listPersonaje.AddRange(from j in cont select new PersonajeListadoDTO { Nombre = j.Nombre, Imagen = j.Imagen });
                    return listPersonaje;
                }
                listPersonaje.AddRange(from j in cont where j.Nombre == name || j.Edad == age select new PersonajeListadoDTO { Nombre = j.Nombre, Imagen = j.Imagen });
                return listPersonaje;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
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
                    Peso = newPersonaje.Peso
                };
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
                var DeletePersonaje = _context.Personaje.First(d => d.Id == id);
                if (DeletePersonaje != null)
                {
                    _context.Personaje.Remove(DeletePersonaje);
                    await _context.SaveChangesAsync();
                    return Ok("Se elimino el personaje.");
                }
                else
                {
                    return NotFound("El personaje no fue encontrado o no existe.");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
