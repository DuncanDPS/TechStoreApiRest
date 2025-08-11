using System.ComponentModel.DataAnnotations;

namespace Servicios.DTOS
{
    public class UsuarioLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Contrasenia { get; set; } = string.Empty;
    }
}
