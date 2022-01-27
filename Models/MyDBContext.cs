using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disney_API.Models
{
    public class MyDBContext:DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options):base(options)
        {
            
        }
        public DbSet<Personaje> Personaje { get; set; }
        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
