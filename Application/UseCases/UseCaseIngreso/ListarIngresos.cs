using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseIngreso
{
    public class ListarIngresos
    {
        private readonly IIngreso _ingreso;

        public ListarIngresos(IIngreso ingreso)
        {
            _ingreso = ingreso;
        }

        public async Task<IEnumerable<Ingreso>> EjecutarAsync()
        {
            return await _ingreso.All();
        }
    }

}
