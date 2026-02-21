// Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaAlmacen.Models;

namespace SistemaAlmacen.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets para tus entidades
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuraciones adicionales
            builder.Entity<Producto>()
                .HasIndex(p => p.Codigo)
                .IsUnique();

            // Datos iniciales (Seed Data)
            builder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Electrónica", Descripcion = "Productos electrónicos" },
                new Categoria { Id = 2, Nombre = "Alimentos", Descripcion = "Productos alimenticios" },
                new Categoria { Id = 3, Nombre = "Limpieza", Descripcion = "Productos de limpieza" }
            );
        }
    }
}