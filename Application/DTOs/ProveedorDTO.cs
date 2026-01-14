using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProveedorDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }
}
