using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;

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

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosProductos()
        {
            try
            {
                var productos = await _productoService.ObtenerTodosLosProductos();
                return Ok(productos); // devuelve 200 OK con la lista de productos
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al recuperar los productos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto(Producto producto)
        {
            if (!ModelState.IsValid) // si no es valido devuelve 400
            {
                return BadRequest(ModelState);
            }
            try
            {
                var productoCreado = await _productoService.CrearProducto(producto);
                return CreatedAtAction(nameof(CrearProducto), new { id = productoCreado.Id }, productoCreado);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }   
        }
    }
}
