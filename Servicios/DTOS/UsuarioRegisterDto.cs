using System.ComponentModel.DataAnnotations;

namespace Servicios.DTOS
{
    public class UsuarioRegisterDto
    {

        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? Rol { get; set; } = "Cliente"; // rol de cliente por defecto
        [Required]
        public string Contrasenia { get; set; } = string.Empty;
    }
}
