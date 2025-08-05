using System.ComponentModel.DataAnnotations;
namespace Servicios.DTOS
{
    public class CategoriaDto
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(500)]
        public string Descripcion { get; set; }
        public List<ProductoAddRequestDto> Productos { get; set; } = new List<ProductoAddRequestDto>();

    }
}
