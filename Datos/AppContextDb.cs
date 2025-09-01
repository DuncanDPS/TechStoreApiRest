// importamos las librerías necesarias
using System;
using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Datos
{
    public class AppContextDb : DbContext
    {
        // Constructor que recibe las opciones de configuración
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {
        }

        // Definimos las entidades que serán mapeadas a la base de datos
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenItem> OrdenItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasPrecision(18, 2);
        }
    }
}
