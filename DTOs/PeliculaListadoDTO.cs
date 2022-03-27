using System;
using System.ComponentModel.DataAnnotations;

namespace Disney_API.DTOs
{
    public class PeliculaListadoDTO
    {
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
