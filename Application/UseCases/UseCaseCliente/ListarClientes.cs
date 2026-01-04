using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseCliente
{
    public class ListarClientes
    {
        private readonly ICliente _cliente;

        public ListarClientes(ICliente cliente)
        {
            _cliente = cliente;
        }

        public async Task<IEnumerable<Cliente>> EjecutarAsync()
        {
            return await _cliente.All();  
        }
    }
}
