using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Servicios.DTOS;
using Servicios.IServicios;
using System.ComponentModel.DataAnnotations;

namespace TechStoreApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Este endpoint hace la funcion de crear una nueva Review de un Producto
        /// </summary>
        /// <param name="review">objecto para crear una review</param>
        /// <returns>devuelve una review creada</returns>
        [HttpPost("crear-review")]
        public async Task<IActionResult> CrearReview(ReviewDtoAddRequest review)
        {
            Log.Information("Intentando Crear un comentario");
            if (!ModelState.IsValid)
            {
                Log.Warning("Modelo Invalido al registrar usuario: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                ReviewDtoResponse response = await _reviewService.CrearReview(review);
                Log.Information("Review Creada con exito");
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                Log.Warning(ex, "Error de validación al crear la review");
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Entidad no encontrada al crear la review");
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "Error de base de datos al crear la review");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sucedio un error inesperado al momento de crear una review");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Error interno del servidor." });
            }
        }

        /// <summary>
        /// Este endpoint obtiene todas las reviews creadas
        /// </summary>
        /// <returns>devuelve una lista con todas las reviews</returns>
        [HttpGet("todas-las-reviews")]
        public async Task<IActionResult> ObtenerTodasLasReviews()
        {
            Log.Information("Intentanto obtener todas las reviews");
            return Ok(await _reviewService.ObtenerTodasLasReviews());
        }

        /// <summary>
        /// Devuelve una review segun el id especificado 
        /// </summary>
        /// <param name="id">id especificado</param>
        /// <returns>devuelve una review segun el id especificado</returns>
        [HttpGet("obtener-review/{id}")]
        public async Task<IActionResult> ObtenerReviewPorId(int id) 
        {
            Log.Information("Intentando obtener un review por su id: {0}",id);

            var review = await _reviewService.ObtenerReviewPorId(id);

            Log.Information("Review obtenida con exito: {0}",review.Titulo);
            return Ok(review);
        }

        /// <summary>
        /// Deletes a review with the specified identifier.
        /// </summary>
        /// <remarks>This method attempts to delete a review identified by <paramref name="id"/>.  If the
        /// review is found and successfully deleted, a no-content response is returned.  If the review does not exist,
        /// a not-found response is returned.</remarks>
        /// <param name="id">The unique identifier of the review to delete. Must be a positive integer.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see
        /// cref="NoContentResult"/> if the review was successfully deleted, or <see cref="NotFoundResult"/> if no
        /// review with the specified identifier exists.</returns>
        [HttpDelete("eliminar-review/{id}")]
        public async Task<IActionResult> EliminarReview(int id)
        {
            Log.Information("Intentando eliminar una review con id: {0}", id);
            var review = await _reviewService.EliminarReview(id);
            if (review)
            {
                Log.Information("Review con id: {0} eliminada con exito", id);
                return NoContent();
            }
            else
            {
                Log.Warning("No se pudo eliminar la review con id: {0}", id);
                return NotFound();
            }
            
        }

        /// <summary>
        /// Updates an existing review with the specified ID using the provided update data.
        /// </summary>
        /// <remarks>This method validates the input model state before attempting the update. If the
        /// model state is invalid,  a <see langword="BadRequest"/> response is returned. The method logs the
        /// operation's progress and errors.</remarks>
        /// <param name="id">The unique identifier of the review to be updated.</param>
        /// <param name="reviewUpdate">An object containing the updated review data. Must not be null and must satisfy validation requirements.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see langword="Ok"/> with
        /// the updated review data if the update is successful. Returns <see langword="BadRequest"/> if the model state
        /// is invalid or if the update fails due to invalid input.</returns>
        [HttpPut("actualizar-review/{id}")]
        public async Task<IActionResult> ActualizarReview(int id, ReviewDtoUpdateRequest reviewUpdate)
        {
            Log.Information("Intentando actualizar una review con el id: {0}", id);
            if (!ModelState.IsValid)
            {
                Log.Warning("Modelo Invalido al registrar usuario: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                ReviewDtoResponse reviewActualizada = await _reviewService.ActualizarReview(id, reviewUpdate);
                Log.Information("Actualizacion de review con id: {0} , hecha con exito", id);
                return Ok(reviewActualizada);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "Error al actualizar la review con id: {id}", id);
                return BadRequest(ex.Message);
            }


        }



    }
}
