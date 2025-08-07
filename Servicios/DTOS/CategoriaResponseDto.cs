using System.ComponentModel.DataAnnotations;
namespace Servicios.DTOS
{
    public class CategoriaResponseDto
    {
        public Guid Id { get; set; }
        
        public string? Nombre { get; set; }
        
        public string? Descripcion { get; set; }
        public List<ProductoResponseDto> Productos { get; set; } = new List<ProductoResponseDto>();

    }
}
