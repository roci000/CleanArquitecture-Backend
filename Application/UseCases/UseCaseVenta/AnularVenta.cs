using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseVenta
{
    public class AnularVenta
    {
        private readonly IVenta _venta;

        public AnularVenta(IVenta venta)
        {
            _venta = venta;
        }

        public async Task EjecutarAsync(Guid id, string motivo)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID de la venta es inválido.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("El motivo de anulación es obligatorio.");

            await _venta.AnularVenta(id, motivo);
        }
    }
}
