using System;
using Entidades;

namespace Servicios.DTOS
{
    public class OrdenDtoAddRequest
    {
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }
        public OrdenEstado Estado { get; set; }
        public ICollection<OrdenItemDtoResponse> Items { get; set; }
    }
}