using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProducto
{
    public class ListarProductos
    {
        private readonly IProducto _producto;

        public ListarProductos(IProducto producto)
        {
            _producto = producto;
        }

        public async Task<IEnumerable<Producto>> EjecutarAsync()
        {
            return await _producto.All();
        }
    }
}
