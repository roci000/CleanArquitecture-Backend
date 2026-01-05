using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Venta
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid EmpleadoId { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal MontoTotal { get; set; }
        public bool Anulado { get; set; } = false;
        public string? MotivoAnulacion { get; set; }
        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}
