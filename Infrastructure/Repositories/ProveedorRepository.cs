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
    public class ProveedorRepository : IProveedor
    {
        private readonly AppDbContext _appDbContext;

        public ProveedorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(Proveedor proveedor)
        {
            _appDbContext.proveedor.Update(proveedor);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Proveedor>> All()
        {
            return await _appDbContext.proveedor.Where(p => p.Estado).ToListAsync();
        }

        public async Task Crear(Proveedor proveedor)
        {
            _appDbContext.proveedor.Add(proveedor);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Proveedor?> ObtenerPorId(Guid id)
        {
            return await _appDbContext.proveedor.FirstOrDefaultAsync(p => p.Id == id && p.Estado); 
        }

        public async Task Eliminar(Guid id)
        {
            var proveedor = await _appDbContext.proveedor.FindAsync(id);
            if (proveedor != null)
            {
                proveedor.Estado = false; 
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistePorNombre(string nombre)
        {
            return await _appDbContext.proveedor.AnyAsync(p => p.Nombre == nombre && p.Estado);
        }
    }
}
