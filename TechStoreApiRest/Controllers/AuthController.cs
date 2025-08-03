using Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.IServicios;
using TechStoreApiRest.DTOS;
using TechStoreApiRest.Mappers;

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
            // 1. Validar usuario y contrasenia
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // mapeamos el usuariodto a entidad
                var usuario = UsuarioMapper.ToEntity(usuarioDto);

                var usuarioCreado = await _usuarioService.RegistrarUsuario(usuario);
                return StatusCode(StatusCodes.Status201Created, usuarioDto); // devuelve 201 Created con el nuevo usuario
            }
            catch (Exception ex)
            {
                return BadRequest(new {error = ex.Message});
            }
        }
    }
}
