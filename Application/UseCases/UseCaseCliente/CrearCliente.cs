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
            await ValidarCliente(cliente);
            await _cliente.Crear(cliente);   
        }

        private async Task ValidarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.NombreCompleto))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            if (!string.IsNullOrWhiteSpace(cliente.Telefono))
            {
                if (cliente.Telefono.Length != 8 || !long.TryParse(cliente.Telefono, out _))
                    throw new ArgumentException("El teléfono debe tener exactamente 8 dígitos numéricos.");
            }

            bool existe = await _cliente.ExistePorNombre(cliente.NombreCompleto.Trim());
            if (existe)
                throw new ArgumentException($"Ya existe un cliente con el nombre '{cliente.NombreCompleto}'.");
        }
    }
}
