using System;



namespace Entidades
{
    public class OrdenItem
    {
        public Guid Id { get; set; }
        public Guid OrdenId { get; set; }
        public Orden Orden { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}