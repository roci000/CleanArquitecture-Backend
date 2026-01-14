using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public decimal PrecioReferencia { get; set; }
        public int StockActual { get; set; }
        public bool Estado { get; set; } = true;
    }
}
