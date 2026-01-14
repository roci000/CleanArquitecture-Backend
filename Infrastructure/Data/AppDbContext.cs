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
        public DbSet<Venta> venta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioReferencia)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Ingreso>()
                .Property(i => i.MontoTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Ingreso>()
                .OwnsMany(i => i.Detalles, detalle =>
                {
                    detalle.WithOwner();
                    detalle.Property(d => d.ProductoId);
                    detalle.Property(d => d.Cantidad).HasPrecision(18, 2);
                    detalle.Property(d => d.PrecioUnitario).HasPrecision(18, 2);
                });

            modelBuilder.Entity<Venta>()
                .Property(v => v.MontoTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Venta>()
                .OwnsMany(v => v.Detalles, detalle =>
                {
                    detalle.WithOwner();
                    detalle.Property(d => d.ProductoId);
                    detalle.Property(d => d.Cantidad).HasPrecision(18, 2);
                    detalle.Property(d => d.PrecioUnitario).HasPrecision(18, 2);
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
