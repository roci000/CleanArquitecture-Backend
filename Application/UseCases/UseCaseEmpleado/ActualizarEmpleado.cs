using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseEmpleado
{
    public class ActualizarEmpleado
    {
        private readonly IEmpleado _empleado;

        public ActualizarEmpleado(IEmpleado empleado)
        {
            _empleado = empleado;
        }

        public async Task EjecutarAsync(Empleado empleado)
        {
            if (empleado.Id == Guid.Empty)
                throw new ArgumentException("El ID del empleado es inválido.");

            ValidarEmpleado(empleado);
            await _empleado.Actualizar(empleado);
        }

        private void ValidarEmpleado(Empleado empleado)
        {
            if (string.IsNullOrWhiteSpace(empleado.Nombre))
                throw new ArgumentException("El nombre del empleado es obligatorio.");

            if (string.IsNullOrWhiteSpace(empleado.Apellido))
                throw new ArgumentException("EEl apellido del empleado es obligatorio..");
        }
    }
}
