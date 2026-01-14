using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmpleado
    {
        Task<IEnumerable<Empleado>> All();
        Task<Empleado?> ObtenerPorId(Guid id);
        Task Crear(Empleado empleado);
        Task Actualizar(Empleado empleado);
        Task Eliminar(Guid id);
        Task<bool> ExistePorNombre(string nombreCompleto);
    }
}
