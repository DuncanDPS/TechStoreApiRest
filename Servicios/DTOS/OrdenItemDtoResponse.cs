using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTOS
{
    public class OrdenItemDtoResponse
    {
        public Guid Id { get; set; }
        public Guid OrdenId { get; set; }
        // Si necesitas mostrar datos de la orden, usa un DTO reducido o solo el Id
        public Guid ProductoId { get; set; }
        public string ProductoNombre { get; set; } // Solo el nombre del producto
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
