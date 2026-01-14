using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseVenta
{
    public class RegistrarVenta
    {
        private readonly IVenta _venta;
        private readonly IProducto _producto;

        public RegistrarVenta(IVenta venta, IProducto producto)
        {
            _venta = venta;
            _producto = producto;
        }

        public async Task EjecutarAsync(Venta venta)
        {
            await ValidarStockDisponible(venta);

            venta.MontoTotal = venta.Detalles.Sum(d => d.Subtotal);

            await _venta.Crear(venta); 
        }

        private async Task ValidarStockDisponible(Venta venta)
        {
            if (venta.ClienteId == Guid.Empty)
                throw new ArgumentException("El cliente es obligatorio.");

            if (venta.EmpleadoId == Guid.Empty)
                throw new ArgumentException("El empleado es obligatorio.");

            if (venta.Detalles == null || !venta.Detalles.Any())
                throw new ArgumentException("La venta debe tener al menos un detalle.");

            foreach (var detalle in venta.Detalles)
            {
                if (detalle.ProductoId == Guid.Empty)
                    throw new ArgumentException("Cada detalle debe tener un producto válido.");

                if (detalle.Cantidad <= 0)
                    throw new ArgumentException("La cantidad debe ser mayor que cero.");

                if (detalle.PrecioUnitario <= 0)
                    throw new ArgumentException("El precio unitario debe ser mayor que cero.");

                var producto = await _producto.ObtenerPorId(detalle.ProductoId);
                if (producto == null)
                    throw new ArgumentException($"El producto con ID {detalle.ProductoId} no existe.");

                if (detalle.Cantidad > producto.StockActual)
                {
                    throw new ArgumentException(
                        $"Stock insuficiente para '{producto.Nombre}'. " +
                        $"Disponible: {producto.StockActual}, solicitado: {detalle.Cantidad}.");
                }
            }
        }
    }
}
