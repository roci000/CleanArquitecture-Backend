using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty; 
        public decimal PrecioReferencia { get; set; }
        public int StockActual { get; set; }
        public bool Estado { get; set; } = true; 
    }
}
