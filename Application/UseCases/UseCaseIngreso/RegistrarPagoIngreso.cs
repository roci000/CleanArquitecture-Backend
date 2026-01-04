using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseIngreso
{
    public class RegistrarPagoIngreso
    {
        private readonly IIngreso _ingreso;

        public RegistrarPagoIngreso(IIngreso ingreso)
        {
            _ingreso = ingreso;
        }

        public async Task EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del ingreso es inválido.");

            await _ingreso.MarcarComoPagado(id, DateTime.Now);
        }
    }
}
