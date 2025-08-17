using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.ComponentModel.DataAnnotations;

namespace Servicios.DTOS
{
    public class ReviewDtoUpdateRequest
    {
        //[Required(ErrorMessage = "El Id de la review es obligatorio")]
        //public int Id { get; set; }

        [Required(ErrorMessage = "El titulo de la Review es obligatorio")]
        [MaxLength(250)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es obligatorio hacer un comentario")]
        [MaxLength(800, ErrorMessage = "No se permiten más de 800 caracteres")]
        public string Comentario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es necesario dejar una calificación del producto")]
        [Range(1, 5)]
        public int Calificacion { get; set; }
    }
}
