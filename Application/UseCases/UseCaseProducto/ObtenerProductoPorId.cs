using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProducto
{
    public class ObtenerProductoPorId
    {
        private readonly IProducto _producto;

        public ObtenerProductoPorId(IProducto producto)
        {
            _producto = producto;
        }

        public async Task<Producto?> EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del producto es inválido.");

            return await _producto.ObtenerPorId(id);
        }
    }
}
