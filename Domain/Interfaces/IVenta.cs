using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IVenta
    {
        Task<IEnumerable<Venta>> All();
        Task<Venta?> ObtenerPorId(Guid id);
        Task Crear(Venta venta);
        Task AnularVenta(Guid id, string motivo);
    }
}
