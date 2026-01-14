using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseEmpleado
{
    public class ListarEmpleados
    {
        private readonly IEmpleado _empleado;

        public ListarEmpleados(IEmpleado empleado)
        {
            _empleado = empleado;
        }

        public async Task<IEnumerable<Empleado>> EjecutarAsync()
        {
            return await _empleado.All();
        }
    }
}
