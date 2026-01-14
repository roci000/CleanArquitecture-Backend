using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProducto
    {
        Task<IEnumerable<Producto>> All();
        Task<Producto?> ObtenerPorId(Guid id);
        Task Crear(Producto producto);
        Task Actualizar(Producto producto);
        Task Eliminar(Guid id);
        Task<bool> ExistePorNombre(string nombre);
        
    }
}
