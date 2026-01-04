using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Producto> producto { get; set; }
        public DbSet<Proveedor> proveedor { get; set; }
        public DbSet<Empleado> empleado { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Ingreso> ingreso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioReferencia)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Ingreso>()
                .OwnsMany(i => i.Detalles, detalle =>
                {
                    detalle.WithOwner();
                    detalle.Property(p => p.ProductoId);
                    detalle.Property(p => p.Cantidad);
                    detalle.Property(p => p.PrecioUnitario);
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
