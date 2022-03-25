using Disney_API.DTOs;
using Disney_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

                var pelicula = _context.Pelicula.ToList();
            List<PeliculaListadoDTO> listPeliculas = new List<PeliculaListadoDTO>();
            if (order=="ASC")
            {
                listPeliculas.AddRange(from p in pelicula orderby p.FechaCreacion ascending select new PeliculaListadoDTO { Titulo = p.Titulo, FechaCreacion = p.FechaCreacion, Imagen = p.Imagen });
                return listPeliculas;
            }
            else if (order=="DESC")
            {
                listPeliculas.AddRange(from p in pelicula orderby p.FechaCreacion descending select new PeliculaListadoDTO { Titulo = p.Titulo, FechaCreacion = p.FechaCreacion, Imagen = p.Imagen });
                return listPeliculas;
            }
            listPeliculas.AddRange(from p in pelicula select new PeliculaListadoDTO { Titulo = p.Titulo, FechaCreacion = p.FechaCreacion, Imagen = p.Imagen });
            return listPeliculas;
        }
        [HttpGet("detalle/{id:int}")]
        public ActionResult<Pelicula> Get(int id)
        {
            return _context.Pelicula.FirstOrDefault(x=>x.Id==id);
        }
        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreateDTO newPeli)
        {
            var pelicula = new Pelicula { 
                Calificación = newPeli.Calificación,
                FechaCreacion = newPeli.FechaCreacion,
                Imagen = newPeli.Imagen,
                Titulo = newPeli.Titulo
            };
            await _context.Pelicula.AddAsync(pelicula);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put(PeliculaUpdateDTO updatePeli)
        {
            var pelicula = new Pelicula
            {
                Id=updatePeli.Id,
                Calificación=updatePeli.Calificación,
                FechaCreacion=updatePeli.FechaCreacion,
                Imagen=updatePeli.Imagen,
                Titulo=updatePeli.Titulo
            };
            _context.Pelicula.Update(pelicula);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id=int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Pelicula pelicula = _context.Pelicula.FirstOrDefault(x => x.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }
            _context.Pelicula.Remove(pelicula);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
