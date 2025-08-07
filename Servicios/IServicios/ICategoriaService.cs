using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Servicios.DTOS;

namespace Servicios.IServicios
{
    /// <summary>
    /// Esta interfaz define los métodos que deben implementarse para el servicio de categorías.
    /// </summary>
    public interface ICategoriaService
    {
        /// <summary>
        /// Obtiene todas las categorías
        /// </summary>
        /// <returns>Devuelve una lista de todas las categorias</returns>
        Task<IEnumerable<CategoriaResponseDto>> ObtenerTodasLasCategorias();
        
        /// <summary>
        /// Obtiene una categoría por su ID.
        /// </summary>
        /// <param name="id">id es el parametro para obtener la categoria especificada</param>
        /// <returns>devuelve una categoria segun su id</returns>
        Task<CategoriaResponseDto> ObtenerCategoriaPorId(Guid id);

        /// <summary>
        /// Crea una nueva categoría.
        /// </summary>
        /// <param name="categoria">objecto de Tipo Categoria que se creara y guardara en la DB</param>
        /// <returns>Devuelve el objeto categoria creado</returns>
        Task<CategoriaResponseDto> CrearCategoria(CategoriaAddRequestDto categoriaAddReq);

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        /// <param name="categoria">categoria especificada para actualizar</param>
        /// <returns>devuelve la categoria actualizada</returns>
        Task<CategoriaResponseDto> ActualizarCategoria(Guid id, CategoriaUpdateRequestDto categoriaUpdateRe);

        /// <summary>
        /// Elimina una categoría por su ID.
        /// </summary>
        /// <param name="id">id de la categoria a eliminar</param>
        /// <returns>devuelve true si la categoria se elimino correctamente</returns>
        Task<bool> EliminarCategoria(Guid id);

    }
}
