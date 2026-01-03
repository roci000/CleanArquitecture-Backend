using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace Application.Mapping
{
    public class MappingProfile:Profile 
    {
        public MappingProfile()
        {
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();
        }
    }
}
