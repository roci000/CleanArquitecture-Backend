using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICliente
    {
        Task<IEnumerable<Cliente>> All();
        Task<Cliente?> ObtenerPorId(Guid id);
        Task Crear(Cliente cliente);
        Task Actualizar(Cliente cliente);
        Task Eliminar(Guid id);
        Task<bool> ExistePorNombre(string nombreCompleto);
    }
}
