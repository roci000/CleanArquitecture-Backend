using Application.DTOs;
using Application.UseCases.UseCaseCliente;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ICliente _cliente;
        private readonly IMapper _mapper;
        private readonly CrearCliente _crearCliente;
        private readonly ActualizarCliente _actualizarCliente;
        private readonly EliminarCliente _eliminarCliente;
        private readonly ObtenerClientePorId _obtenerClientePorId;
        private readonly ListarClientes _listarClientes;

        public ClienteController(
            ICliente cliente,
            IMapper mapper,
            CrearCliente crearCliente,
            ActualizarCliente actualizarCliente,
            EliminarCliente eliminarCliente,
            ObtenerClientePorId obtenerClientePorId,
            ListarClientes listarClientes)
        {
            _cliente = cliente;
            _mapper = mapper;
            _crearCliente = crearCliente;
            _actualizarCliente = actualizarCliente;
            _eliminarCliente = eliminarCliente;
            _obtenerClientePorId = obtenerClientePorId;
            _listarClientes = listarClientes;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _listarClientes.EjecutarAsync();

            if (!clientes.Any())
            {
                return NotFound("No hay clientes que listar");
            }

            var response = clientes.Select(c => new
            {
                c.Id,
                c.NombreCompleto,
                c.Telefono,
                c.Direccion,
                c.Estado
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cliente = await _obtenerClientePorId.EjecutarAsync(id);
            if (cliente == null)
                return NotFound(new { mensaje = "Cliente no encontrado" });

            var clienteDto = _mapper.Map<ClienteDTO>(cliente);
            return Ok(clienteDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                cliente.Id = Guid.NewGuid();

                await _crearCliente.EjecutarAsync(cliente);

                return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, clienteDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                cliente.Id = id;

                await _actualizarCliente.EjecutarAsync(cliente);

                return Ok(new { mensaje = "Cliente actualizado correctamente" });
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
                await _eliminarCliente.EjecutarAsync(id);
                return Ok(new { mensaje = "Cliente eliminado correctamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
