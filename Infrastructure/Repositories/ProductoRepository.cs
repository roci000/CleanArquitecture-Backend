using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductoRepository : IProducto
    {
        private readonly AppDbContext _appDbContext;

        public ProductoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(Producto producto)
        {
            _appDbContext.producto.Update(producto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> All()
        {
            return await _appDbContext.producto.Where(p => p.Estado).ToListAsync();
        }

        public async Task Crear(Producto producto)
        {
            _appDbContext.producto.Add(producto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Eliminar(Guid id)
        {
            var producto = await _appDbContext.producto.FindAsync(id);
            if (producto != null)
            {
                producto.Estado = false;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Producto?> ObtenerPorId(Guid id)
        {
            return await _appDbContext.producto.FirstOrDefaultAsync(p => p.Id == id && p.Estado); 
        }

        public async Task<bool> ExistePorNombre(string nombre)
        {
            return await _appDbContext.producto.AnyAsync(p => p.Nombre == nombre && p.Estado);
        }
    }
}
