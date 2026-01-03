using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProducto
{
    public class EliminarProducto
    {
        private readonly IProducto _producto;

        public EliminarProducto(IProducto producto)
        {
            _producto = producto;
        }

        public async Task EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del producto es inválido.");

            await _producto.Eliminar(id);
        }
    }
}
