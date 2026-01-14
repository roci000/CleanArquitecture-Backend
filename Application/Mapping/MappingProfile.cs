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
            CreateMap<Empleado, EmpleadoDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<DetalleIngreso, DetalleIngresoDTO>().ReverseMap();
            CreateMap<Ingreso, IngresoDTO>().ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles)).ReverseMap(); ;
            CreateMap<DetalleVenta, DetalleVentaDTO>().ReverseMap();
            CreateMap<Venta, VentaDTO>().ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles)).ReverseMap();
        }
    }
}
