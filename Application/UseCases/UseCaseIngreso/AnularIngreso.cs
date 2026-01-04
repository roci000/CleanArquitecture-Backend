using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseIngreso
{
    public class AnularIngreso
    {
        private readonly IIngreso _ingreso;

        public AnularIngreso(IIngreso ingreso)
        {
            _ingreso = ingreso;
        }

        public async Task EjecutarAsync(Guid id, string motivo)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del ingreso es inválido.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("El motivo de anulación es obligatorio.");

            await _ingreso.AnularIngreso(id, motivo);
        }
    }
}
