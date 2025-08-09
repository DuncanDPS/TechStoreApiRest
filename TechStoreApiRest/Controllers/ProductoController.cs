using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.DTOS;
using Servicios.DTOS.Mappers;

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
        //[Authorize(Policy ="UserPolicy")]
        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodosLosProductos()
        {
            try
            {
                var productos = await _productoService.ObtenerTodosLosProductos();
        
                return Ok(productos); // devuelve 200 OK con la lista de productos mapeados
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
                ProductoResponseDto producto = await _productoService.ObtenerProductoPorId(id);
                return Ok(producto);
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
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearProducto(ProductoAddRequestDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var productoCreado = await _productoService.CrearProducto(productoDto);
                // Opcional: puedes devolver el DTO mapeado del producto creado
                
                return CreatedAtAction(nameof(CrearProducto), new { id = productoCreado.Id }, productoCreado);
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
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> ActualizarProducto(Guid id, ProductoUpdateRequestDto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // obtener un producto con ese id
                ProductoResponseDto response = await _productoService.ObtenerProductoPorId(id);
          
                // se actualiza el producto
                ProductoResponseDto productoActualizado = await _productoService.ActualizarProducto(id, producto);

                return Ok(productoActualizado);
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
        [Authorize(Policy = "AdminPolicy")]
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
