using Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using Servicios.DTOS;
using Serilog;

namespace TechStoreApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar-usuario")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioRegisterDto usuarioDto)
        {
            Log.Information("Intentando Registrar un Usuario con email: {Email}", usuarioDto.Email);
            // 1. Validar usuario y contrasenia
            if (!ModelState.IsValid)
            {
                Log.Warning("Modelo Invalido al registrar usuario: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                
                var usuarioCreado = await _usuarioService.RegistrarUsuario(usuarioDto);
                Log.Information("Usuario Registrado exitosamente: {Email}", usuarioDto.Email);
                return StatusCode(StatusCodes.Status201Created, usuarioDto); // devuelve 201 Created con el nuevo usuario
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al registrar usuario: {Email}", usuarioDto.Email);
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
