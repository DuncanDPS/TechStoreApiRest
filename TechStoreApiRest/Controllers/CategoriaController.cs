using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechStoreApiRest.Mappers;



namespace TechStoreApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        // Inyeccion de dependencias del servicio de categoria
        private readonly Servicios.IServicios.ICategoriaService _categoriaService;
        public CategoriaController(Servicios.IServicios.ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Crea una nueva categoria.
        /// </summary>
        /// <param name="categoria">Categoria que se creara</param>
        /// <returns>devuelve la categoria creada</returns>
        [HttpPost("CrearCategoria")]
        public async Task<IActionResult> CrearCategoria(Categoria categoria)
        {
            // validamos el modelo recibido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);// code 400
            }
            try
            {
                // llamamos al servicio para crear la categoria
                var categoriaCreada = await _categoriaService.CrearCategoria(categoria);
                return CreatedAtAction(nameof(CrearCategoria), new { id = categoriaCreada.Id }, categoriaCreada); // code 201
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // code 400
            }
        }

        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodasLasCategorias()
        {
            // devueve todas las categorias usando dto
            try
            {
                var categorias = await _categoriaService.ObtenerTodasLasCategorias();
                var categoriasDto = categorias.Select(c => c.ToDto()).ToList(); // mapeamos las categorias a CategoriaDto
                return Ok(categoriasDto); // devuelve 200 OK con la lista de categorias mapeadas
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al recuperar las categorias: {ex.Message}");
            }
        }
    }
}
