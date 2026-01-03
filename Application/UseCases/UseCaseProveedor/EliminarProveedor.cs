using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProveedor
{
    public class EliminarProveedor
    {
        private readonly IProveedor _proveedor;

        public EliminarProveedor(IProveedor proveedor)
        {
            _proveedor = proveedor;
        }

        public async Task EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del proveedor es inválido.");

            await _proveedor.Eliminar(id);
        }
    }
}
