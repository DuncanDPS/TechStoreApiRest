using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.DTOS;
using Servicios.DTOS.Mappers;




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
        /// <param name="categoriaDto">Categoria que se creara</param>
        /// <returns>devuelve la categoria creada</returns>
        [HttpPost("CrearCategoria")]
        public async Task<IActionResult> CrearCategoria(CategoriaAddRequestDto categoriaDto)
        {
            // validamos el modelo recibido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);// code 400
            }
            try
            {
                var CategoriaCreada = await _categoriaService.CrearCategoria(categoriaDto);
                return Ok(CategoriaCreada);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // code 400
            }
        }

        /// <summary>
        /// Obtiene todas las categorias.
        /// </summary>
        /// <returns>Devuelve todas las categorias existentes</returns>
        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodasLasCategorias()
        {
            // devueve todas las categorias usando dto
            try
            {
                return Ok(await _categoriaService.ObtenerTodasLasCategorias());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al recuperar las categorias: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene una categoria por su ID.
        /// </summary>
        /// <param name="id">id de la categoria a recuperar</param>
        /// <returns>devuelve la categoria segun el id indicado</returns>
        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerCategoriaPorId(Guid id)
        {
            try
            {
                var categoria = await _categoriaService.ObtenerCategoriaPorId(id);
                if (categoria == null)
                {
                    return NotFound($"Categoria con ID {id} no encontrada.");
                }
                return Ok(categoria); // devuelve 200 OK 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // code 404
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al recuperar la categoria: {ex.Message}");
            }
        }

        [HttpPut("actualizarCategoria/{id}")]
        public async Task<IActionResult> ActualizarCategoria(Guid id, [FromBody] CategoriaUpdateRequestDto categoriaUpdateRequest)
        {
            //if (id != )
            //{
            //    return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            //}
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var categoriaActualizada = await _categoriaService.ActualizarCategoria(id,categoriaUpdateRequest);
                return Ok(categoriaActualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Elimina una categoria segun el id Especificado
        /// </summary>
        /// <param name="id">id especifico de una categoria</param>
        /// <returns>devuelve true si la categoria fue eliminada</returns>
        [HttpDelete("EliminarCategoria/{id}")]
        public async Task<IActionResult> EliminarCategoria(Guid id)
        {
            try
            {
                var eliminado = await _categoriaService.EliminarCategoria(id);
                if (!eliminado)
                {
                    return NotFound($"La Categoria con id {id}  no fue encontrada");
                }
                return NoContent(); // se elimino la categoria
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar la categoria: {ex.Message}");
            }
        }
    }
}
