using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseVenta
{
    public class ListarVentas
    {
        private readonly IVenta _venta;

        public ListarVentas(IVenta venta)
        {
            _venta = venta;
        }

        public async Task<IEnumerable<Venta>> EjecutarAsync()
        {
            return await _venta.All();
        }
    }
}
