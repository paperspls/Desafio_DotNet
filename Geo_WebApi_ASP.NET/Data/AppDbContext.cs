using Geo_WebApi_ASP.NET.Model;
using Microsoft.EntityFrameworkCore;

namespace Geo_WebApi_ASP.NET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Localidade>().ToTable("IBGE");
            modelBuilder.Entity<User>().ToTable("Usuario");


            /* Relacionamento Localidade -> User
            modelBuilder.Entity<Localidade>()
                    .HasOne(l => l.Usuario)
                    .WithMany(u => u.Localidade)
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);*/
        }

        public DbSet<Localidade> Localidades { get; set; } = null!;
        public DbSet<User> Usuario { get; set; } = null!;

    }
}

