using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney_API.DTOs
{
    public class PersonajeDetalleDTO : PersonajeDTO
    {
        public List<PeliculaDTO> Peliculas { get; set; }
    }
}
