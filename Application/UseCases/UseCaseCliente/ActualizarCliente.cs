using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseCliente
{
    public class ActualizarCliente
    {
        private readonly ICliente _cliente;

        public ActualizarCliente(ICliente cliente)
        {
            _cliente = cliente;
        }    

        public async Task EjecutarAsync(Cliente cliente)
        {
            if (cliente.Id == Guid.Empty)
                throw new ArgumentException("El ID del cliente es inválido.");

            ValidarCliente(cliente);
            await _cliente.Actualizar(cliente);
        }

        private void ValidarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.NombreCompleto))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            if (!string.IsNullOrWhiteSpace(cliente.Telefono))
            {
                if (cliente.Telefono.Length != 8 || !long.TryParse(cliente.Telefono, out _))
                    throw new ArgumentException("El teléfono debe tener exactamente 8 dígitos numéricos.");
            }
        }
    }
}
