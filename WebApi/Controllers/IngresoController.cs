using Application.DTOs;
using Application.UseCases.UseCaseIngreso;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoController : ControllerBase
    {
        private readonly IIngreso _ingreso;
        private readonly IProducto _producto;
        private readonly IMapper _mapper;
        private readonly RegistrarIngreso _registrarIngreso;
        private readonly ListarIngresos _listarIngresos;
        private readonly ObtenerIngresoPorId _obtenerIngresoPorId;
        private readonly RegistrarPagoIngreso _registrarPagoIngreso;
        private readonly AnularIngreso _anularIngreso; 

        public IngresoController(
            IIngreso ingreso,
            IProducto producto,
            IMapper mapper,
            RegistrarIngreso registrarIngreso,
            ListarIngresos listarIngresos,
            ObtenerIngresoPorId obtenerIngresoPorId,
            RegistrarPagoIngreso registrarPagoIngreso,
            AnularIngreso anularIngreso) 
        {
            _ingreso = ingreso;
            _producto = producto;
            _mapper = mapper;
            _registrarIngreso = registrarIngreso;
            _listarIngresos = listarIngresos;
            _obtenerIngresoPorId = obtenerIngresoPorId;
            _registrarPagoIngreso = registrarPagoIngreso;
            _anularIngreso = anularIngreso;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ingresos = await _listarIngresos.EjecutarAsync();
            if (!ingresos.Any()) return NotFound("No hay ingresos");

            var response = ingresos.Select(i => new {
                i.Id,
                i.ProveedorId,
                i.EmpleadoId,
                i.FechaIngreso,
                i.MontoTotal,
                i.Pagado,
                i.Anulado
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ingreso = await _obtenerIngresoPorId.EjecutarAsync(id);
            if (ingreso == null)
                return NotFound(new { mensaje = "Ingreso no encontrado" });

            var ingresoDto = _mapper.Map<Ingreso>(ingreso);
            return Ok(ingresoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] IngresoDTO ingresoDTO)
        {
            try
            {
                var ingreso = _mapper.Map<Ingreso>(ingresoDTO);
                ingreso.FechaIngreso = DateTime.Now;
                ingreso.Anulado = false; 

                await _registrarIngreso.EjecutarAsync(ingreso);

                return CreatedAtAction(nameof(GetById), new { id = ingreso.Id }, ingreso);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id}/pagar")]
        public async Task<IActionResult> RegistrarPago(Guid id)
        {
            try
            {
                await _registrarPagoIngreso.EjecutarAsync(id);
                return Ok(new { mensaje = "Pago registrado correctamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id}/anular")]
        public async Task<IActionResult> Anular(Guid id, [FromBody] string motivo)
        {
            try
            {
                await _anularIngreso.EjecutarAsync(id, motivo);
                return Ok(new { mensaje = "Ingreso anulado correctamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
