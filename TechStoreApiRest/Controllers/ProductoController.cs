using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using TechStoreApiRest.DTOS;
using TechStoreApiRest.Mappers;

namespace TechStoreApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // inyeccion de servicios
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>
        /// Recupera todos los productos.
        /// </summary>
        /// <returns>Retorna todos los productos </returns>
        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodosLosProductos()
        {
            try
            {
                var productos = await _productoService.ObtenerTodosLosProductos();
                // aplicamos el mapeo de Producto a ProductoDto
                var productosDto = productos.Select(p => p.ToDto()).ToList();
                return Ok(productosDto); // devuelve 200 OK con la lista de productos mapeados
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al recuperar los productos: {ex.Message}");
            }
        }

        /// <summary>
        /// Devuelve un producto por su ID.
        /// </summary>
        /// <param name="id">Id del producto que se necesita devolver</param>
        /// <returns>devuelve el producto solicitado mediante su id unico</returns>
        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerProductoPorId(Guid id)
        {
            try
            {
                var producto = await _productoService.ObtenerProductoPorId(id);
                if(producto == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado.");
                }
                // aplicamos el mapeo de Producto a ProductoDto
                var productoDto = producto.ToDto();
                return Ok(productoDto); // devuelve 200 OK con el producto mapeado
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al recuperar el producto: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="productoDto"></param>
        /// <returns></returns>
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Mapeo de DTO a entidad
                var producto = productoDto.ToEntity();
                var productoCreado = await _productoService.CrearProducto(producto);

                // Opcional: puedes devolver el DTO mapeado del producto creado
                var productoCreadoDto = productoCreado.ToDto();
                return CreatedAtAction(nameof(CrearProducto), new { id = productoCreadoDto.Id }, productoCreadoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">Id del producto que se desea actualizar</param>
        /// <param name="productoDto">producto que se desea actualizar</param>
        /// <returns>devuelve el producto actualizado</returns>
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> ActualizarProducto(Guid id, [FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Mapeo de DTO a entidad
                var producto = productoDto.ToEntity();
                producto.Id = id; // Aseguramos que el Id del producto sea el correcto
                var productoActualizado = await _productoService.ActualizarProducto(producto);
                if (productoActualizado == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado.");
                }
                // Opcional: puedes devolver el DTO mapeado del producto actualizado
                var productoActualizadoDto = productoActualizado.ToDto();
                return Ok(productoActualizadoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">Id del producto a eliminar</param>
        /// <returns>devuelve 204 no content si se elimino correctamente, de lo contrario devuelve 500</returns>
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarProducto(Guid id)
        {
            try
            {
                var eliminado = await _productoService.EliminarProducto(id);
                if (!eliminado)
                {
                    return NotFound($"Producto con ID {id} no encontrado.");
                }
                return NoContent(); // devuelve 204 No Content si se eliminó correctamente
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el producto: {ex.Message}");
            }
        }
    }
}
