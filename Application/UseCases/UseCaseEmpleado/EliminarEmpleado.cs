using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseEmpleado
{
    public class EliminarEmpleado
    {
        private readonly IEmpleado _empleado;

        public EliminarEmpleado(IEmpleado empleado)
        {
            _empleado = empleado;
        }

        public async Task EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del empleado es inválido.");

            await _empleado.Eliminar(id);
        }
    }
}
