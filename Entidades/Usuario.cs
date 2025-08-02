using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {

        public Guid Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La longitud del nombre debe ser menor a 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [StringLength(150, ErrorMessage = "La longitud de los apellidos debe ser menor a 150 caracteres")]
        public string Apellidos { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Debe colocar una direccion de email valida")]
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "Cliente"; // rol de cliente por defecto
        [Required]
        [StringLength(255,ErrorMessage = ("La Contrasenia debe ser menor de 255 caracteres"))]
        public string ContraseniaHash { get; set; } = string.Empty;

    }
}
