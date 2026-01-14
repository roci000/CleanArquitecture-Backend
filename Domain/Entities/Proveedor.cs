using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Proveedor
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Telefono { get; set; } 
        public string Direccion { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }
}
