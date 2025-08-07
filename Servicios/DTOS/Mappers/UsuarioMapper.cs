using Servicios.DTOS;
using Entidades;
namespace Servicios.DTOS.Mappers
{
    /// <summary>
    /// Proporciona funcionalidad para mapear datos relacionados con usuarios entre diferentes representaciones.
    /// </summary>
    /// <remarks>Esta clase se utiliza normalmente para convertir datos de usuario entre modelos de dominio, DTOs u otros formatos. Sirve como una utilidad para asegurar transformaciones consistentes de la información del usuario.</remarks>
    public static class UsuarioMapper
    {
       
        public static Usuario ToEntity(UsuarioRegisterDto usuarioRegister)
        {
            if (usuarioRegister == null)
                throw new ArgumentNullException(nameof(usuarioRegister));

            return new Usuario
            {
                Nombre = usuarioRegister.Nombre,
                Apellidos = usuarioRegister.Apellidos,
                Email = usuarioRegister.Email,
                Rol = string.IsNullOrWhiteSpace(usuarioRegister.Rol) ? "Cliente" : usuarioRegister.Rol,
                ContraseniaHash = usuarioRegister.Contrasenia // Aquí deberías hashear la contraseña en un caso real
            };
        }
        // Pasa el usaurio de entidad a Response
        public static UsuarioResponse EntityToResponse(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            return new UsuarioResponse
            {
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                Rol = usuario.Rol
            };
        }


    }
}
