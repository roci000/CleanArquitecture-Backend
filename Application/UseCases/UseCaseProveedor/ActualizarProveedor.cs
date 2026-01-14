using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProveedor
{
    public class ActualizarProveedor
    {
        private readonly IProveedor _proveedor;

        public ActualizarProveedor(IProveedor proveedor)
        {
            _proveedor = proveedor;
        }

        public async Task EjecutarAsync(Proveedor proveedor)
        {
            if (proveedor.Id == Guid.Empty)
                throw new ArgumentException("El ID del proveedor es inválido.");

            ValidarProveedor(proveedor);
            await _proveedor.Actualizar(proveedor);
        }

        private void ValidarProveedor(Proveedor proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor.Nombre))
                throw new ArgumentException("El nombre del proveedor no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(proveedor.Direccion))
                throw new ArgumentException("La dirección del proveedor no puede estar vacía.");

            if (!string.IsNullOrWhiteSpace(proveedor.Telefono))
            {
                if (proveedor.Telefono.Length != 8 || !long.TryParse(proveedor.Telefono, out _))
                    throw new ArgumentException("El teléfono debe tener exactamente 8 dígitos numéricos.");
            }
        }
    }
}
