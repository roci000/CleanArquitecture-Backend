using Application.DTOs;
using Application.UseCases.UseCaseEmpleado;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleado _empleado;
        private readonly IMapper _mapper;
        private readonly CrearEmpleado _crearEmpleado;
        private readonly ActualizarEmpleado _actualizarEmpleado;
        private readonly EliminarEmpleado _eliminarEmpleado;
        private readonly ObtenerEmpleadoPorId _obtenerEmpleadoPorId;
        private readonly ListarEmpleados _listarEmpleados;

        public EmpleadoController(
            IEmpleado empleado,
            IMapper mapper,
            CrearEmpleado crearEmpleado,
            ActualizarEmpleado actualizarEmpleado,
            EliminarEmpleado eliminarEmpleado,
            ObtenerEmpleadoPorId obtenerEmpleadoPorId,
            ListarEmpleados listarEmpleados)
        {
            _empleado = empleado;
            _mapper = mapper;
            _crearEmpleado = crearEmpleado;
            _actualizarEmpleado = actualizarEmpleado;
            _eliminarEmpleado = eliminarEmpleado;
            _obtenerEmpleadoPorId = obtenerEmpleadoPorId;
            _listarEmpleados = listarEmpleados;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var empleados = await _listarEmpleados.EjecutarAsync();

            if (!empleados.Any())
            {
                return NotFound("No hay empleados que listar");
            }

            var response = empleados.Select(e => new
            {
                e.Id,
                e.Nombre,
                e.Apellido,
                e.Cargo,
                e.Estado
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var empleado = await _obtenerEmpleadoPorId.EjecutarAsync(id);
            if (empleado == null)
                return NotFound(new { mensaje = "Empleado no encontrado" });

            var empleadoDto = _mapper.Map<EmpleadoDTO>(empleado);
            return Ok(empleadoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmpleadoDTO empleadoDTO)
        {
            try
            {
                var empleado = _mapper.Map<Empleado>(empleadoDTO);
                empleado.Id = Guid.NewGuid();

                await _crearEmpleado.EjecutarAsync(empleado);

                return CreatedAtAction(nameof(GetById), new { id = empleado.Id }, empleadoDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmpleadoDTO empleadoDTO)
        {
            try
            {
                var empleado = _mapper.Map<Empleado>(empleadoDTO);
                empleado.Id = id;

                await _actualizarEmpleado.EjecutarAsync(empleado);

                return Ok(new { mensaje = "Empleado actualizado correctamente" });
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
                await _eliminarEmpleado.EjecutarAsync(id);
                return Ok(new { mensaje = "Empleado eliminado correctamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
