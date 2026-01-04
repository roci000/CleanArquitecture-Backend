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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioReferencia)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
