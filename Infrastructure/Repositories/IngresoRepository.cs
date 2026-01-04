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
    internal class IngresoRepository : IIngreso
    {
        private readonly AppDbContext _appDbContext;

        public IngresoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Crear(Ingreso ingreso)
        {
            ingreso.Id = Guid.NewGuid();
            _appDbContext.ingreso.Add(ingreso);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ingreso>> All()
        {
            return await _appDbContext.ingreso.Include(i => i.Detalles).Where(i => !i.Anulado).ToListAsync();
        }

        public async Task<Ingreso?> ObtenerPorId(Guid id)
        {
            return await _appDbContext.ingreso.Include(i => i.Detalles).FirstOrDefaultAsync(i => i.Id == id && !i.Anulado); // ← No devuelve anulados
        }

        public async Task MarcarComoPagado(Guid id, DateTime fechaPago)
        {
            var ingreso = await _appDbContext.ingreso.FindAsync(id);
            if (ingreso != null && !ingreso.Anulado)
            {
                if (ingreso.Pagado)
                    throw new InvalidOperationException("El ingreso ya está pagado.");

                ingreso.Pagado = true;
                ingreso.FechaPago = fechaPago;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task AnularIngreso(Guid id, string motivo)
        {
            var ingreso = await _appDbContext.ingreso.FindAsync(id);
            if (ingreso == null)
                throw new ArgumentException("Ingreso no encontrado.");

            if (ingreso.Pagado)
                throw new InvalidOperationException("No se puede anular un ingreso que ya está pagado.");

            if (ingreso.Anulado)
                throw new InvalidOperationException("El ingreso ya está anulado.");

            ingreso.Anulado = true;
            ingreso.MotivoAnulacion = motivo;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
