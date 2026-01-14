using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProveedor
{
    public class ListarProveedores
    {
        private readonly IProveedor _proveedor;

        public ListarProveedores(IProveedor proveedor)
        {
            _proveedor = proveedor;
        }

        public async Task<IEnumerable<Proveedor>> EjecutarAsync()
        {
            return await _proveedor.All();
        }
    }
}
