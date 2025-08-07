using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Servicios.DTOS; 

namespace Servicios.IServicios
{
    /// <summary>
    ///Esta interfaz define los métodos que deben implementarse para el servicio de productos.
    /// 
    /// </summary>
    public interface IProductoService
    {
        /// <summary>
        /// Recupera todos los productos.
        /// </summary>
        /// <returns>Todos los productos</returns>
        Task<IEnumerable<ProductoResponseDto>> ObtenerTodosLosProductos();

        /// <summary>
        /// Recupera un producto por su ID.
        /// </summary>
        /// <param name="id">Se refiere al Guid como Id, necesario para buscar el producto</param>
        /// <returns>Un producto segun el Id</returns>
        Task<ProductoResponseDto> ObtenerProductoPorId(Guid id);

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="producto">Se refiere al producto que va a ser anadido a la base de datos </param>
        /// <returns>devuelve el producto creado</returns>
        Task<ProductoResponseDto> CrearProducto(ProductoAddRequestDto producto);

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="producto">Producto existente que se desea actualizar</param>
        /// <returns>devuelve el producto actualizado</returns>
        Task<ProductoResponseDto> ActualizarProducto(Guid id, ProductoUpdateRequestDto producto);

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">Id del producto a ser eliminado</param>
        /// <returns>true si el producto se elimino, de lo contrario false</returns>
        Task<bool> EliminarProducto(Guid id);
    }
}
