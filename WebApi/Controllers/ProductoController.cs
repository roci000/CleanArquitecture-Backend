using Application.DTOs;
using Application.UseCases.UseCaseProducto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProducto _productoRepository;
        private readonly IMapper _mapper;
        private readonly CrearProducto _crearProducto;
        private readonly ActualizarProducto _actualizarProducto;
        private readonly EliminarProducto _eliminarProducto;
        private readonly ObtenerProductoPorId _obtenerProductoPorId;
        private readonly ListarProductos _listarProductos;

        public ProductoController(
            IProducto productoRepository,
            IMapper mapper,
            CrearProducto crearProducto,
            ActualizarProducto actualizarProducto,
            EliminarProducto eliminarProducto,
            ObtenerProductoPorId obtenerProductoPorId,
            ListarProductos listarProductos)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _crearProducto = crearProducto;
            _actualizarProducto = actualizarProducto;
            _eliminarProducto = eliminarProducto;
            _obtenerProductoPorId = obtenerProductoPorId;
            _listarProductos = listarProductos;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _listarProductos.EjecutarAsync();

            if (!productos.Any())
                return NotFound("No hay productos que listar");

            var resultado = productos.Select(p => new
            {
                id = p.Id,
                nombre = p.Nombre,
                unidadMedida = p.UnidadMedida,
                precioReferencia = p.PrecioReferencia,
                stockActual = p.StockActual,
                estado = p.Estado
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var producto = await _obtenerProductoPorId.EjecutarAsync(id);
            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            var productoDto = _mapper.Map<ProductoDTO>(producto);
            return Ok(productoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDTO productoDTO)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDTO);
                producto.Id = Guid.NewGuid(); 

                await _crearProducto.EjecutarAsync(producto);

                return CreatedAtAction(nameof(GetById), new { id = producto.Id }, productoDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductoDTO productoDTO)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDTO);
                producto.Id = id;

                await _actualizarProducto.EjecutarAsync(producto);

                return Ok(new { mensaje = "Producto actualizado correctamente" });
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
                await _eliminarProducto.EjecutarAsync(id);
                return Ok(new { mensaje = "Producto eliminado correctamente" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

