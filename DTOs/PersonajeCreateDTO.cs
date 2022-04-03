using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Disney_API.DTOs
{
    public class PersonajeCreateDTO
    {
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public float Peso { get; set; }
        [Required]
        public string Historia { get; set; }
        [Required]
        public List<int> Peliculas { get; set; } 
    }
}
