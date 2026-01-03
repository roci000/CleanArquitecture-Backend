using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProveedor
{
    public class ObtenerProveedorPorId
    {
        private readonly IProveedor _proveedor;

        public ObtenerProveedorPorId(IProveedor proveedor)
        {
            _proveedor = proveedor;
        }

        public async Task<Proveedor?> EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del proveedor es inválido.");

            return await _proveedor.ObtenerPorId(id);
        }
    }
}
