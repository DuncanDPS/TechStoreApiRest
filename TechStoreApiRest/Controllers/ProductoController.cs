using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.DTOS;
using Servicios.DTOS.Mappers;
using Serilog;

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
            Log.Information("Intentando obtener todos los producto");
            try
            {
                var productos = await _productoService.ObtenerTodosLosProductos();
                Log.Information("Se obtuvieron todos los productos con exito");
                return Ok(productos); // devuelve 200 OK con la lista de productos mapeados
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al intentar obtener todos los productos");
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
            Log.Information("Intentando obtener un producto con id: {id}", id);
            try
            {
                ProductoResponseDto producto = await _productoService.ObtenerProductoPorId(id);
                Log.Information("Producto obtenido con exito: {0}", producto.Nombre);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                Log.Error("Error al intentar obtener un producto con el ID: {id}",id);
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
            Log.Information("Intentando Crear un producto: {Nombre}", productoDto.Nombre);
            if (!ModelState.IsValid)
            {
                Log.Warning("Modelo Invalido al registrar usuario: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }
            try
            {

                var productoCreado = await _productoService.CrearProducto(productoDto);
                // Opcional: puedes devolver el DTO mapeado del producto creado
                Log.Information("Producto creado con exito: producto : {Nombre}",productoCreado.Nombre);
                return CreatedAtAction(nameof(CrearProducto), new { id = productoCreado.Id }, productoCreado);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "Error al intentar crear un prducto: {Nombre}",productoDto.Nombre);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">Id del producto que se desea actualizar</param>
        /// <param name="producto">producto que se desea actualizar</param>
        /// <returns>devuelve el producto actualizado</returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> ActualizarProducto(Guid id, ProductoUpdateRequestDto producto)
        {
            Log.Information("Intentando Actualizar Producto con el id: {id}",id);
            if (!ModelState.IsValid)
            {
                Log.Warning("Modelo Invalido al registrar usuario: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                // obtener un producto con ese id
                ProductoResponseDto response = await _productoService.ObtenerProductoPorId(id);
          
                // se actualiza el producto
                ProductoResponseDto productoActualizado = await _productoService.ActualizarProducto(id, producto);
                Log.Information("Producto actualizado con exito, producto con id: {id}", id);
                return Ok(productoActualizado);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "Error al actualizar el producto con id: {id}", id);
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
            Log.Information("Intentando Eliminar un producto con id: {id}", id);
            try
            {
                var eliminado = await _productoService.EliminarProducto(id);
                if (!eliminado)
                {
                    Log.Warning("No se pudo eliminar un producto con este id: {id}",id);
                    return NotFound($"Producto con ID {id} no encontrado.");
                }
                Log.Information("Producto con {id} ,eliminado con exito",id);
                return NoContent(); // devuelve 204 No Content si se eliminó correctamente
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al intentar eliminar un producto con id: {id}",id);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el producto: {ex.Message}");
            }
        }
    }
}
