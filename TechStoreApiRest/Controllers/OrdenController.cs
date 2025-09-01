using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.DTOS;
using Servicios.IServicios;
using Serilog;

namespace TechStoreApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private IOrdenService _ordenService;

        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        // implementa el metodo para crear una orden
        [HttpPost("crear-orden")]
        public async Task<IActionResult> crearOrden(OrdenDtoAddRequest orden)
        {
            Log.Information("Intentando crear una orden");
            if(!ModelState.IsValid) 
            {
                Log.Warning("Modelo Invalido al crear orden: {@ModelState}", ModelState);
                return BadRequest(ModelState);// code 400
            }
            try
            {
                var ordenCreada = await _ordenService.CrearOrden(orden);
                Log.Information("Orden Creada con exito");
                return CreatedAtAction(nameof(crearOrden), new { id = ordenCreada.Id }, ordenCreada);
            }
            catch (Exception ex) 
            {
                Log.Error(ex, "Sucedio un error al intentar crear una orden");
                return BadRequest(ex.Message);
            }
        }


    }
}
