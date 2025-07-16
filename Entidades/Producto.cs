// data annotations
using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Producto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(150, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        // Relación con Categoria
        [Required]
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }
}
