using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Disney_API.DTOs
{
    public class PeliculaUpdateDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public int Calificación { get; set; }
        [Required]
        public int GeneroId { get; set; }
        public List<int> Personajes { get; set; }
    }
}
