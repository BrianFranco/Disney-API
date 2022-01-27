using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Disney_API.Models
{
    public class Personaje
    {
        public int Id { get; set; }
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
        public ICollection<Pelicula> Peliculas { get; set; }
    }

    public class Pelicula
    {
        public int Id { get; set; }
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public int Calificación { get; set; }
        public ICollection<Personaje> Personajes { get; set; }
    }
    public class Genero
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Imagen { get; set; }
        public ICollection<Pelicula> Peliculas { get; set; }
    }
    public class Usuario
    {
        [Required]
        [EmailAddress]
        [MinLength(8)]
        [Key]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }
}
