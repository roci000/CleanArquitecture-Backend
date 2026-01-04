using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseCliente
{
    public class CrearCliente
    {
        private readonly ICliente _cliente;

        public CrearCliente(ICliente cliente)
        {
            _cliente = cliente;
        }

        public async Task EjecutarAsync(Cliente cliente)
        {
            ValidarCliente(cliente);
            await _cliente.Crear(cliente);   
        }

        private void ValidarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.NombreCompleto))
                throw new ArgumentException("El nombre del cliente es obligatorio.");
        }
    }
}
