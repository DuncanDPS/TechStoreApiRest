using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTOS
{
    public class ProductoResponseDto
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Guid CategoriaId { get; set; }
        public string? CategoriaNombre { get; set; }
    }
}
