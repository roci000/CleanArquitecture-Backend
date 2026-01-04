using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseIngreso
{
    public class ObtenerIngresoPorId
    {
        private readonly IIngreso _ingreso;

        public ObtenerIngresoPorId(IIngreso ingreso)
        {
            _ingreso = ingreso;
        }

        public async Task<Ingreso?> EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del ingreso es inválido.");

            return await _ingreso.ObtenerPorId(id);
        }
    }
}
