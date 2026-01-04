using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseEmpleado
{
    public class ObtenerEmpleadoPorId
    {
        private readonly IEmpleado _empleado;

        public ObtenerEmpleadoPorId(IEmpleado empleado)
        {
            _empleado = empleado;
        }

        public async Task<Empleado?> EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del empleado es inválido.");

            return await _empleado.ObtenerPorId(id);
        }
    }
}
