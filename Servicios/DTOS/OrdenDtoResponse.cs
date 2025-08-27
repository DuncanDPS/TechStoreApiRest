using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enums;


namespace Servicios.DTOS
{
    public class OrdenDtoResponse
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }
        public OrdenEstado Estado { get; set; }
        public ICollection<OrdenItemDtoResponse>? Items { get; set; }
    }
}
