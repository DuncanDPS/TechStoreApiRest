using Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.DTOS;


namespace TechStoreApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;
        //private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            //_tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost("registrar-usuario")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioRegisterDto usuarioDto)
        {
            // 1. Validar usuario y contrasenia
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                var usuarioCreado = await _usuarioService.RegistrarUsuario(usuarioDto);
                
                return StatusCode(StatusCodes.Status201Created, usuarioDto); // devuelve 201 Created con el nuevo usuario
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("iniciar-sesion")]
        public async Task<IActionResult> IniciarSesion(UsuarioLoginDto usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var usuarioLog = await _usuarioService.IniciarSesion(usuario);
                return StatusCode(StatusCodes.Status201Created, usuarioLog);
            }
            catch (Exception ex)
            {
                return BadRequest(new {error = ex.Message});
            }
        }
    }
}
