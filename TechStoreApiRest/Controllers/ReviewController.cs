using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.DTOS;
using Serilog;

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

            ReviewDtoResponse response = await _reviewService.CrearReview(review);
            Log.Information("Review Creada con exito");
            return Ok(response);
        }

        [HttpGet("todas-las-reviews")]
        public async Task<IActionResult> ObtenerTodasLasReviews()
        {
            Log.Information("Intentanto obtener todas las reviews");
            return Ok(await _reviewService.ObtenerTodasLasReviews());
        }

        [HttpGet("obtener-review/{id}")]
        public async Task<IActionResult> ObtenerReviewPorId(int id) 
        {
            Log.Information("Intentando obtener un review por su id: {0}",id);

            var review = await _reviewService.ObtenerReviewPorId(id);

            Log.Information("Review obtenida con exito: {0}",review.Titulo);
            return Ok(review);
        }


    }
}
