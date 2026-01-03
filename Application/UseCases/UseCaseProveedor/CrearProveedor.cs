using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UseCaseProveedor
{
    public class CrearProveedor
    {
        private readonly IProveedor _proveedor;

        public CrearProveedor(IProveedor proveedor)
        {
            _proveedor = proveedor;
        }

        public async Task EjecutarAsync(Proveedor proveedor)
        {
            ValidarProveedor(proveedor);
            await _proveedor.Crear(proveedor);
        }

        private void ValidarProveedor(Proveedor proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor.Nombre))
                throw new ArgumentException("El nombre del proveedor no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(proveedor.Direccion))
                throw new ArgumentException("La dirección del proveedor no puede estar vacía.");
        }
    }
}
