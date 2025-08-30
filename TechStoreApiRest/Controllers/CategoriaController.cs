using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.DTOS;
using Servicios.DTOS.Mappers;
using Serilog;
using Servicios.IServicios;



namespace TechStoreApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        // Inyeccion de dependencias del servicio de categoria
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Crea una nueva categoria.
        /// </summary>
        /// <param name="categoriaDto">Categoria que se creara</param>
        /// <returns>devuelve la categoria creada</returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CrearCategoria")]
        public async Task<IActionResult> CrearCategoria(CategoriaAddRequestDto categoriaDto)
        {
            Log.Information("Intentando Crear una nueva categoria: {Nombre} ", categoriaDto.Nombre);
            // validamos el modelo recibido
            if (!ModelState.IsValid)
            {
                Log.Warning("Modelo Invalido al registrar categoria: {@ModelState}", ModelState);
                return BadRequest(ModelState);// code 400
            }
            try
            {
                var CategoriaCreada = await _categoriaService.CrearCategoria(categoriaDto);
                Log.Information("Categoria Creada con Exito: {Nombre}", CategoriaCreada.Nombre);
                return Ok(CategoriaCreada);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "Error al intentar crear una categoria: {Nombre}", categoriaDto.Nombre);
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
            Log.Information("Intentando obtener todas las categorias");
            // devueve todas las categorias usando dto
            try
            {
                var categorias = await _categoriaService.ObtenerTodasLasCategorias();
                Log.Information("Todas las categorias fueron obtenidas con exito");
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al intentar obtener todas las categorias");
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
            Log.Information("Intentando obtener la categoria con ID: {Id}", id);
            try
            {
                var categoria = await _categoriaService.ObtenerCategoriaPorId(id);
                if (categoria == null)
                {
                    Log.Warning("Categoria con ID {Id} no encontrada.", id);
                    return NotFound($"Categoria con ID {id} no encontrada.");
                }
                Log.Information("Categoria con ID {Id} obtenida con exito.", id);
                return Ok(categoria); // devuelve 200 OK 
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error(ex, "KeyNotFoundException al obtener categoria con ID: {Id}", id);
                return NotFound(ex.Message); // code 404
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al recuperar la categoria con ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al recuperar la categoria: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza una categoría existente según el ID especificado.
        /// </summary>
        /// <param name="id">ID de la categoría a actualizar.</param>
        /// <param name="categoriaUpdateRequest">Datos actualizados de la categoría.</param>
        /// <returns>Devuelve la categoría actualizada si la operación es exitosa; de lo contrario, un mensaje de error.</returns>
        [HttpPut("actualizarCategoria/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ActualizarCategoria(Guid id, [FromBody] CategoriaUpdateRequestDto categoriaUpdateRequest)
        {
            Log.Information("Intentando actualizar una categoria con ID: {0}", id);
            if (!ModelState.IsValid)
            {
                Log.Warning("Modelo Invalido al actualizar: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                var categoriaActualizada = await _categoriaService.ActualizarCategoria(id,categoriaUpdateRequest);
                Log.Information("Actualizacion de categoria con id: {0} , hecha con exito", id);
                return Ok(categoriaActualizada);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al intentar actualizar la categoria con id: {0}", id);
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Elimina una categoria segun el id Especificado
        /// </summary>
        /// <param name="id">id especifico de una categoria</param>
        /// <returns>devuelve true si la categoria fue eliminada</returns>
        [HttpDelete("EliminarCategoria/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> EliminarCategoria(Guid id)
        {
            Log.Information("Intentando Eliminar una categoria con ID: {0}", id);

            try
            {
                var eliminado = await _categoriaService.EliminarCategoria(id);
                if (!eliminado)
                {
                    Log.Warning("No se pudo eliminar una categoria con el id: {0}", id);
                    return NotFound($"La Categoria con id {id}  no fue encontrada");
                }
                Log.Information("La categoria con id: {0} se elimino con exito", id);
                return NoContent(); // se elimino la categoria
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al intentar eliminar una categoria con el id: {0}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar la categoria: {ex.Message}");
            }
        }
    }
}
