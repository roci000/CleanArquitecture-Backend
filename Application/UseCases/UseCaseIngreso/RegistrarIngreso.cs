using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseIngreso
{
    public class RegistrarIngreso
    {
        private readonly IIngreso _ingreso;
        private readonly IProducto _producto;

        public RegistrarIngreso(IIngreso ingreso, IProducto producto)
        {
            _ingreso = ingreso;
            _producto = producto;
        }

        public async Task EjecutarAsync(Ingreso ingreso)
        {
            await ValidarIngreso(ingreso);

            ingreso.MontoTotal = ingreso.Detalles.Sum(d => d.Subtotal);

            await _ingreso.Crear(ingreso);

            foreach (var detalle in ingreso.Detalles)
            {
                var producto = await _producto.ObtenerPorId(detalle.ProductoId);
                if (producto != null)
                {
                    decimal margen = 1.30m; 
                    producto.PrecioReferencia = Math.Round(detalle.PrecioUnitario * margen, 2);
                    await _producto.Actualizar(producto);
                }
            }
        }

        private async Task ValidarIngreso(Ingreso ingreso)
        {
            if (ingreso.ProveedorId == Guid.Empty)
                throw new ArgumentException("El proveedor es obligatorio.");

            if (ingreso.EmpleadoId == Guid.Empty)
                throw new ArgumentException("El empleado es obligatorio.");

            if (ingreso.Detalles == null || !ingreso.Detalles.Any())
                throw new ArgumentException("El ingreso debe tener al menos un detalle.");

            foreach (var detalle in ingreso.Detalles)
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
            }
        }
    }
}