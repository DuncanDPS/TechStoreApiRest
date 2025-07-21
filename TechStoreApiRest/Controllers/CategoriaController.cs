using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



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

        [HttpPost]
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

    }
}
