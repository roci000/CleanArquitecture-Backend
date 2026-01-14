using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseVenta
{
    public class ObtenerVentaPorId
    {
        private readonly IVenta _venta;

        public ObtenerVentaPorId(IVenta venta)
        {
            _venta = venta;
        }

        public async Task<Venta?> EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID de la venta es inválido.");

            return await _venta.ObtenerPorId(id);
        }
    }
}
