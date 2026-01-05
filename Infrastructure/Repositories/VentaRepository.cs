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
    public class VentaRepository : IVenta
    {
        private readonly AppDbContext _appDbContext;
        private readonly IProducto _producto;

        public VentaRepository(AppDbContext appDbContext, IProducto producto)
        {
            _appDbContext = appDbContext;
            _producto = producto;
        }

        public async Task Crear(Venta venta)
        {
            venta.Id = Guid.NewGuid();

            foreach (var detalle in venta.Detalles)
            {
                var producto = await _producto.ObtenerPorId(detalle.ProductoId);
                producto.StockActual -= (int)detalle.Cantidad;
                await _producto.Actualizar(producto);
            }

            _appDbContext.venta.Add(venta);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Venta>> All()
        {
            return await _appDbContext.venta
                .Include(v => v.Detalles)
                .Where(v => !v.Anulado)
                .ToListAsync();
        }

        public async Task<Venta?> ObtenerPorId(Guid id)
        {
            return await _appDbContext.venta
                .Include(v => v.Detalles)
                .FirstOrDefaultAsync(v => v.Id == id && !v.Anulado);
        }

        public async Task AnularVenta(Guid id, string motivo)
        {
            var venta = await _appDbContext.venta
                .Include(v => v.Detalles)
                .FirstOrDefaultAsync(v => v.Id == id && !v.Anulado);

            if (venta == null)
                throw new ArgumentException("Venta no encontrada o ya anulada.");

            if (DateTime.Now.Subtract(venta.FechaVenta).TotalMinutes > 5)
                throw new InvalidOperationException("No se puede anular una venta después de 5 minutos.");

            foreach (var detalle in venta.Detalles)
            {
                var producto = await _producto.ObtenerPorId(detalle.ProductoId);
                if (producto != null)
                {
                    producto.StockActual += (int)detalle.Cantidad;
                    await _producto.Actualizar(producto);
                }
            }

            venta.Anulado = true;
            venta.MotivoAnulacion = motivo;
            await _appDbContext.SaveChangesAsync();
        }
    }

}