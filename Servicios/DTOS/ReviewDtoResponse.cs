using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTOS
{
    public class ReviewDtoResponse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se necesita un ID de usuario")]
        public Guid UsuarioId { get; set; }
        [Required(ErrorMessage = "Se necesita un ID de producto")]
        public Guid ProductoId { get; set; }
        [Required(ErrorMessage = "El titulo de la Review es obligatorio")]
        [MaxLength(250)]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Es obligatorio hacer un comentario")]
        [MaxLength(800, ErrorMessage = "No se permiten mas de 800 caracteres")]
        public string Comentario { get; set; } = string.Empty;
        [Required(ErrorMessage = "Es necesario dejar una calificacion del producto")]
        [Range(1, 5)]
        public int Calificacion { get; set; } // 1 a 5
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string NombreDeUsuario { get; set; } = string.Empty;
        public string NombreDeProducto { get; set; } = string.Empty;
    }
}
