using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VentaDTO
    {
        public Guid ClienteId { get; set; }
        public Guid EmpleadoId { get; set; }
        public DateTime FechaVenta { get; set; }
        public List<DetalleVentaDTO> Detalles { get; set; } = new();
    }
}
