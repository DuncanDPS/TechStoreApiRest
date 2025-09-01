using System;
using System.ComponentModel.DataAnnotations;
using Entidades;
using Entidades.Enums;
namespace Servicios.DTOS
{
    public class OrdenDtoAddRequest
    {
        [Required]
        public Guid UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        [Required]
        public decimal Total { get; set; }
        [Required]
        public OrdenEstado Estado { get; set; }
        [Required]
        public ICollection<OrdenItemDtoAddRequest> Items { get; set; }
    }
}