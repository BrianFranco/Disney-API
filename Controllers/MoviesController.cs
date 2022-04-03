using Disney_API.DTOs;
using Disney_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MoviesController : ControllerBase
    {
        private readonly MyDBContext _context;
        public MoviesController(MyDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<PeliculaListadoDTO>> Get([FromQuery] String name, [FromQuery] int genero, [FromQuery] string order)
        {
            try
            {
                var pelicula = _context.Pelicula.ToList();
                if (name != null)
                {
                    pelicula = (from peli in pelicula where peli.Titulo.ToLower().Contains(name.ToLower()) select peli).ToList();  
                }
                if(order != null)
                {
                    if (order == "ASC")
                    {
                        pelicula = pelicula.OrderBy(ord => ord.FechaCreacion).ToList();
                    }else if (order == "DESC")
                    {
                        pelicula = pelicula.OrderByDescending(ord => ord.FechaCreacion).ToList();
                    }
                }
                if (genero != 0)
                {
                    pelicula = (from peli in pelicula where peli.GeneroId == genero select peli).ToList();
                }

                if (pelicula == null || pelicula.Count == 0)
                {
                    return NotFound();
                }

                return (from peli in pelicula select new PeliculaListadoDTO {
                    Titulo = peli.Titulo, 
                    Imagen = peli.Imagen,
                    FechaCreacion = peli.FechaCreacion 
                }).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("detalle/{id:int}")]
        public ActionResult<PeliculaDetalleDTO> Get(int id)
        {
            var pelicula = _context.Pelicula.Include(x => x.Personajes).Include(x=>x.Genero).FirstOrDefault(x => x.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return new PeliculaDetalleDTO
            {
                Id = pelicula.Id,
                Imagen = pelicula.Imagen,
                Calificación = pelicula.Calificación,
                Titulo = pelicula.Titulo,
                FechaCreacion = pelicula.FechaCreacion,
                Genero = new GeneroDTO
                {
                    Id = pelicula.Genero.Id,
                    Nombre = pelicula.Genero.Nombre,
                    Imagen = pelicula.Imagen
                },
                Personajes= (from pers in pelicula.Personajes select new PersonajeDTO
                {
                    Id=pers.Id,
                    Nombre=pers.Nombre,
                    Edad=pers.Edad,
                    Historia=pers.Historia,
                    Imagen=pers.Imagen,
                    Peso=pers.Peso
                }).ToList()
            };
        }
        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreateDTO newPeli)
        {
            var pelicula = new Pelicula
            {
                Calificación = newPeli.Calificación,
                FechaCreacion = newPeli.FechaCreacion,
                Imagen = newPeli.Imagen,
                Titulo = newPeli.Titulo,
                GeneroId = newPeli.GeneroId
            };
            pelicula.Genero = _context.Genero.FirstOrDefault(x => x.Id == newPeli.GeneroId);
            if (pelicula.Genero == null)
            {
                return BadRequest("Debe ingresar un IdGenero valido.");
            }
            if (newPeli != null)
            {
                foreach (int idPersonaje in newPeli.Personajes)
                {
                    var p = await _context.Personaje.FirstOrDefaultAsync(x => x.Id == idPersonaje);
                    if ( p == null){
                        return BadRequest($"Uno de los personajes ingresados no existe o aun no esta cargado ID: {idPersonaje}");
                    }
                    pelicula.Personajes.Add(p);
               }
            }
            await _context.Pelicula.AddAsync(pelicula);
            await _context.SaveChangesAsync();
            return Ok($"Se creo correctamente la pelicula {pelicula.Titulo}");
        }
        [HttpPut]
        public async Task<ActionResult> Put(PeliculaUpdateDTO updatePeli)
        {
            var pelicula = new Pelicula
            {
                Id = updatePeli.Id,
                Calificación = updatePeli.Calificación,
                FechaCreacion = updatePeli.FechaCreacion,
                Imagen = updatePeli.Imagen,
                Titulo = updatePeli.Titulo,
                GeneroId = updatePeli.GeneroId
            };
            pelicula.Genero = _context.Genero.FirstOrDefault(x => x.Id == updatePeli.GeneroId);
            if (pelicula.Genero == null)
            {
                return BadRequest("Debe ingresar un IdGenero valido.");
            }
            if (updatePeli.Personajes != null)
            {
                var peli = _context.Pelicula.Include(x => x.Personajes).ToList();
                if (updatePeli.Personajes.Count >= peli.Count)
                {
                    return BadRequest("Ingreso menos personajes de los ya guardados.");
                }
                foreach (int idPersonaje in updatePeli.Personajes)
                {
                    var p = await _context.Personaje.FirstOrDefaultAsync(x => x.Id == idPersonaje);
                    if (p == null)
                    {
                        return BadRequest($"Uno de los personajes ingresados no existe o aun no esta cargado ID: {idPersonaje}");
                    }
                    pelicula.Personajes.Add(p);
                }
            }
            _context.Pelicula.Update(pelicula);
            await _context.SaveChangesAsync();
            return Ok($"Se modifico correctamente la pelicula{pelicula.Titulo}");
        }
        [HttpDelete("{id=int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Pelicula pelicula = _context.Pelicula.FirstOrDefault(x => x.Id == id);
            if (pelicula == null)
            {
                return BadRequest("No se encontro la pelicula indicada.");
            }
            _context.Pelicula.Remove(pelicula);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
