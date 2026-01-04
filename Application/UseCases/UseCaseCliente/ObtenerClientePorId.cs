using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseCliente
{
    public class ObtenerClientePorId
    {
        private readonly ICliente _cliente;

        public ObtenerClientePorId(ICliente cliente)
        {
            _cliente = cliente;
        }

        public async Task<Cliente?> EjecutarAsync(Guid id)
        {   
            if (id == Guid.Empty)
                throw new ArgumentException("El ID del cliente es inválido.");

            return await _cliente.ObtenerPorId(id);
        }
    }
}
