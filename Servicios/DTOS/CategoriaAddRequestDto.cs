using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTOS
{
    public class CategoriaAddRequestDto
    {
        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }
        //public List<ProductoResponseDto> Productos { get; set; } = new List<ProductoResponseDto>();
    }
}
