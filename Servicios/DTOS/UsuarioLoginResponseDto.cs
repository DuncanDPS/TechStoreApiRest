using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTOS
{
    public class UsuarioLoginResponseDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        //public string? Rol { get; set; } = "Cliente"; // rol de cliente por defecto
        public string? Token {get; set;} = string.Empty;
    }
}
