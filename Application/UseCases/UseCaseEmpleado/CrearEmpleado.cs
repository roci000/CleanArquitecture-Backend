using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseEmpleado
{
    public class CrearEmpleado
    {
        private readonly IEmpleado _empleado;
        public CrearEmpleado(IEmpleado empleado)
        {
            _empleado = empleado;
        }

        public async Task EjecutarAsync(Empleado empleado)
        {
            ValidarEmpleado(empleado);
            await _empleado.Crear(empleado);
        }

        private void ValidarEmpleado(Empleado empleado)
        {
            if (string.IsNullOrWhiteSpace(empleado.Nombre))
                throw new ArgumentException("El nombre del empleado es obligatorio.");

            if (string.IsNullOrWhiteSpace(empleado.Apellido))
                throw new ArgumentException("El apellido del empleado es obligatorio.");
        }
    }
}
