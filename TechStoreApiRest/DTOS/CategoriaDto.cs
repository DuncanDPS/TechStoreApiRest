using System.ComponentModel.DataAnnotations;
namespace TechStoreApiRest.DTOS
{
    public class CategoriaDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(500)]
        public string Descripcion { get; set; }
        public List<ProductoDto> Productos { get; set; } = new List<ProductoDto>();

    }
}
