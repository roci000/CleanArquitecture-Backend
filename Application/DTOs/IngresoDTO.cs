using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class IngresoDTO
    {
        public Guid ProveedorId { get; set; }
        public Guid EmpleadoId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public List<DetalleIngresoDTO> Detalles { get; set; } = new();
    }
}
