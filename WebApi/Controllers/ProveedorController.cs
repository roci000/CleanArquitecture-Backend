using Application.DTOs;
using Application.UseCases.UseCaseProveedor;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedor _proveedorRepository;
        private readonly IMapper _mapper;
        private readonly CrearProveedor _crearProveedor;
        private readonly ActualizarProveedor _actualizarProveedor;
        private readonly EliminarProveedor _eliminarProveedor;
        private readonly ObtenerProveedorPorId _obtenerProveedorPorId;
        private readonly ListarProveedores _listarProveedores;

        public ProveedorController(
            IProveedor proveedorRepository,
            IMapper mapper,
            CrearProveedor crearProveedor,
            ActualizarProveedor actualizarProveedor,
            EliminarProveedor eliminarProveedor,
            ObtenerProveedorPorId obtenerProveedorPorId,
            ListarProveedores listarProveedores)
        {
            _proveedorRepository = proveedorRepository;
            _mapper = mapper;
            _crearProveedor = crearProveedor;
            _actualizarProveedor = actualizarProveedor;
            _eliminarProveedor = eliminarProveedor;
            _obtenerProveedorPorId = obtenerProveedorPorId;
            _listarProveedores = listarProveedores;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proveedores = await _listarProveedores.EjecutarAsync();

            if (!proveedores.Any())
            {
                return NotFound("No hay proveedores que listar");
            }

            var response = proveedores.Select(p => new
            {
                p.Id,
                p.Nombre,
                p.Telefono,
                p.Direccion,
                p.Estado
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var proveedor = await _obtenerProveedorPorId.EjecutarAsync(id);
            if (proveedor == null)
                return NotFound(new { mensaje = "Proveedor no encontrado" });

            var proveedorDto = _mapper.Map<ProveedorDTO>(proveedor);
            return Ok(proveedorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProveedorDTO proveedorDTO)
        {
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorDTO);
                proveedor.Id = Guid.NewGuid();

                await _crearProveedor.EjecutarAsync(proveedor);

                return CreatedAtAction(nameof(GetById), new { id = proveedor.Id }, proveedorDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProveedorDTO proveedorDTO)
        {
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorDTO);
                proveedor.Id = id;

                await _actualizarProveedor.EjecutarAsync(proveedor);

                return Ok(new { mensaje = "Proveedor actualizado correctamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _eliminarProveedor.EjecutarAsync(id);
                return Ok(new { mensaje = "Proveedor eliminado correctamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
