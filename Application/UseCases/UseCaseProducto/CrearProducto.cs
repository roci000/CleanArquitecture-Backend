using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProducto
{
    public class CrearProducto
    {
        private readonly IProducto _producto;
        public CrearProducto(IProducto producto)
        {
            _producto = producto;
        }

        public async Task EjecutarAsync(Producto producto)
        {
            await ValidarProducto(producto);
            await _producto.Crear(producto);
        }

        private async Task ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(producto.UnidadMedida))
                throw new ArgumentException("La unidad de medida no puede estar vacía.");

            if (producto.PrecioReferencia <= 0)
                throw new ArgumentException("El precio de referencia debe ser mayor que cero.");

            if (producto.StockActual < 0)
                throw new ArgumentException("El stock no puede ser negativo.");

            bool existe = await _producto.ExistePorNombre(producto.Nombre.Trim());
            if (existe)
                throw new ArgumentException($"Ya existe un producto con el nombre '{producto.Nombre}'.");
        }
    }
}
