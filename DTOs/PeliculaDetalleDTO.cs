using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disney_API.DTOs
{
    public class PeliculaDetalleDTO:PeliculaDTO
    {
        public List<PersonajeDTO> Personajes { get; set; }
    }
}
