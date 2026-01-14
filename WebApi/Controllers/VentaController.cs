using Application.DTOs;
using Application.UseCases.UseCaseVenta;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVenta _venta;
        private readonly IProducto _producto;
        private readonly IMapper _mapper;
        private readonly RegistrarVenta _registrarVenta;
        private readonly ListarVentas _listarVentas;
        private readonly ObtenerVentaPorId _obtenerVentaPorId;
        private readonly AnularVenta _anularVenta;

        public VentaController(
            IVenta venta,
            IProducto producto,
            IMapper mapper,
            RegistrarVenta registrarVenta,
            ListarVentas listarVentas,
            ObtenerVentaPorId obtenerVentaPorId,
            AnularVenta anularVenta)
        {
            _venta = venta;
            _producto = producto;
            _mapper = mapper;
            _registrarVenta = registrarVenta;
            _listarVentas = listarVentas;
            _obtenerVentaPorId = obtenerVentaPorId;
            _anularVenta = anularVenta;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ventas = await _listarVentas.EjecutarAsync();

            if (!ventas.Any())
            {
                return NotFound(new { mensaje = "No hay ventas que listar" });
            }

            var response = ventas.Select(v => new
            {
                v.Id,
                v.ClienteId,
                v.EmpleadoId,
                v.FechaVenta,
                v.MontoTotal,
                v.Anulado
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var venta = await _obtenerVentaPorId.EjecutarAsync(id);
            if (venta == null)
                return NotFound(new { mensaje = "Venta no encontrada" });

            var ventaDto = _mapper.Map<Venta>(venta);
            return Ok(ventaDto);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] VentaDTO ventaDTO)
        {
            try
            {
                var venta = _mapper.Map<Venta>(ventaDTO);
                venta.FechaVenta = DateTime.Now;
                venta.Anulado = false;

                await _registrarVenta.EjecutarAsync(venta);

                return CreatedAtAction(nameof(GetById), new { id = venta.Id }, venta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id}/anular")]
        public async Task<IActionResult> Anular(Guid id, [FromBody] string motivo)
        {
            try
            {
                await _anularVenta.EjecutarAsync(id, motivo);
                return Ok(new { mensaje = "Venta anulada correctamente. El stock ha sido restaurado." });
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
