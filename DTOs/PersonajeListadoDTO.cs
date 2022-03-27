using System.ComponentModel.DataAnnotations;

namespace Disney_API.DTOs
{
    public class PersonajeListadoDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Imagen { get; set; }
    }
}
