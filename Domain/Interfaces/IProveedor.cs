using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProveedor
    {
        Task<IEnumerable<Proveedor>> All();
        Task<Proveedor?> ObtenerPorId(Guid id);
        Task Crear(Proveedor proveedor);
        Task Actualizar(Proveedor proveedor);
        Task Eliminar(Guid id);
        Task<bool> ExistePorNombre(string nombre);
    }
}
