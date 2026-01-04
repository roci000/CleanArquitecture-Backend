using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseCliente
{
    public class EliminarCliente
    {
        private readonly ICliente _cliente;

        public EliminarCliente(ICliente cliente)
        {
            _cliente = cliente;
        }

        public async Task EjecutarAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del cliente es inválido.");

            await _cliente.Eliminar(id);  
        }
    }
}
