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
    public class EmpleadoRepository : IEmpleado
    {
        private readonly AppDbContext _appDbContext;

        public EmpleadoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(Empleado empleado)
        {
            _appDbContext.empleado.Update(empleado);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Empleado>> All()
        {
            return await _appDbContext.empleado.Where(e => e.Estado).ToListAsync();
        }

        public async Task Crear(Empleado empleado)
        {
            _appDbContext.empleado.Add(empleado);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Empleado?> ObtenerPorId(Guid id)
        {
            return await _appDbContext.empleado.FirstOrDefaultAsync(e => e.Id == id && e.Estado);
        }

        public async Task Eliminar(Guid id)
        {
            var empleado = await _appDbContext.empleado.FindAsync(id);
            if (empleado != null)
            {
                empleado.Estado = false;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistePorNombre(string nombreCompleto)
        {
            return await _appDbContext.empleado.AnyAsync(p => p.Nombre == nombreCompleto && p.Estado);
        }
    }
}
