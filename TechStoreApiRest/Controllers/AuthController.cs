//using Datos;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Servicios.IServicios;


//namespace TechStoreApiRest.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {

//        private readonly IUsuarioService _usuarioService;
//        private readonly ITokenGeneratorService _tokenGeneratorService;

//        public AuthController(IUsuarioService usuarioService, ITokenGeneratorService tokenGeneratorService)
//        {
//            _usuarioService = usuarioService;
//            _tokenGeneratorService = tokenGeneratorService;
//        }

//        [HttpPost("registrar-usuario")]
//        public async Task<IActionResult> RegistrarUsuario(UsuarioRegisterDto usuarioDto)
//        {
//            // 1. Validar usuario y contrasenia
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            try
//            {
//                // mapeamos el usuariodto a entidad
//                var usuario = UsuarioMapper.CategoriaDtoRespToEntity(usuarioDto);

//                var usuarioCreado = await _usuarioService.RegistrarUsuario(usuario);
//                var usuarioResp = UsuarioMapper.ToEntityToResponse(usuario);
//                return StatusCode(StatusCodes.Status201Created, usuarioResp); // devuelve 201 Created con el nuevo usuario
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new {error = ex.Message});
//            }
//        }

//        [HttpPost("iniciar-sesion")]
//        public async Task<IActionResult> IniciarSesion(UsuarioLoginDto usuario)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            try
//            {

//            }
//        }
//    }
//}
