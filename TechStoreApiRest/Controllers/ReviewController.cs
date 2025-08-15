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
        



    }
}
