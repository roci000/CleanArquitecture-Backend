using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IIngreso
    {
        Task<IEnumerable<Ingreso>> All();
        Task<Ingreso?> ObtenerPorId(Guid id);
        Task Crear(Ingreso ingreso);
        Task MarcarComoPagado(Guid id, DateTime fechaPago);
        Task AnularIngreso(Guid id, string motivo);
    }
}
