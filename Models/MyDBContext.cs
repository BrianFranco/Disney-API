using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Disney_API.Models
{
    public class MyDBContext : IdentityDbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }
        public DbSet<Personaje> Personaje { get; set; }
        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Genero> Genero { get; set; }
    }
}
