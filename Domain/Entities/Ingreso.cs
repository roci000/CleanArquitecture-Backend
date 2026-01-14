using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ingreso
    {
        public Guid Id { get; set; }
        public Guid ProveedorId { get; set; }
        public Guid EmpleadoId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal MontoTotal { get; set; }
        public bool Pagado { get; set; } = false;
        public DateTime? FechaPago { get; set; }
        public bool Anulado { get; set; } = false;
        public string? MotivoAnulacion { get; set; }
        public List<DetalleIngreso> Detalles { get; set; } = new();

        public DateTime FechaLimitePago => FechaIngreso.AddDays(3);
        public bool EstaVencido => !Pagado && !Anulado && DateTime.Now.Date > FechaLimitePago.Date;
    }
}
