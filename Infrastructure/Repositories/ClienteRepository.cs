using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : ICliente
    {
        private readonly AppDbContext _appDbContext;

        public ClienteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(Cliente cliente)
        {
            _appDbContext.cliente.Update(cliente);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cliente>> All()
        {
            return await _appDbContext.cliente.Where(c => c.Estado).ToListAsync();
        }

        public async Task Crear(Cliente cliente)
        {
            _appDbContext.cliente.Add(cliente);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Cliente?> ObtenerPorId(Guid id)
        {
            return await _appDbContext.cliente.FirstOrDefaultAsync(c => c.Id == id && c.Estado);
        }

        public async Task Eliminar(Guid id)
        {
            var cliente = await _appDbContext.cliente.FindAsync(id);
            if (cliente != null)
            {
                cliente.Estado = false;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistePorNombre(string nombreCompleto)
        {
            return await _appDbContext.cliente.AnyAsync(p => p.NombreCompleto == nombreCompleto && p.Estado);
        }
    }
}
