using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IngresoRepository : IIngreso
    {
        private readonly AppDbContext _appDbContext;
        private readonly IProducto _productoRepository; // ← inyectado

        public IngresoRepository(AppDbContext appDbContext, IProducto productoRepository)
        {
            _appDbContext = appDbContext;
            _productoRepository = productoRepository; // ← asignado
        }

        public async Task Crear(Ingreso ingreso)
        {
            ingreso.Id = Guid.NewGuid();

            foreach (var detalle in ingreso.Detalles)
            {
                var producto = await _productoRepository.ObtenerPorId(detalle.ProductoId);
                if (producto != null)
                {
                    producto.StockActual += (int)detalle.Cantidad;
                    await _productoRepository.Actualizar(producto);
                }
            }

            _appDbContext.ingreso.Add(ingreso);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ingreso>> All()
        {
            return await _appDbContext.ingreso
                .Include(i => i.Detalles)
                .Where(i => !i.Anulado)
                .ToListAsync();
        }

        public async Task<Ingreso?> ObtenerPorId(Guid id)
        {
            return await _appDbContext.ingreso
                .Include(i => i.Detalles)
                .FirstOrDefaultAsync(i => i.Id == id && !i.Anulado);
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
            var ingreso = await _appDbContext.ingreso
                .Include(i => i.Detalles) 
                .FirstOrDefaultAsync(i => i.Id == id && !i.Anulado);

            if (ingreso == null)
                throw new ArgumentException("Ingreso no encontrado.");

            if (ingreso.Pagado)
                throw new InvalidOperationException("No se puede anular un ingreso que ya está pagado.");

            if (ingreso.Anulado)
                throw new InvalidOperationException("El ingreso ya está anulado.");

            foreach (var detalle in ingreso.Detalles)
            {
                var producto = await _productoRepository.ObtenerPorId(detalle.ProductoId);
                if (producto != null)
                {
                    producto.StockActual -= (int)detalle.Cantidad; 
                    await _productoRepository.Actualizar(producto);
                }
            }

            ingreso.Anulado = true;
            ingreso.MotivoAnulacion = motivo;
            await _appDbContext.SaveChangesAsync();
        }
    }
}