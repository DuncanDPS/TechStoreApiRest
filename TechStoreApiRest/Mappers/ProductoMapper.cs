using Entidades;
using TechStoreApiRest.DTOS;

namespace TechStoreApiRest.Mappers
{
    public static class ProductoMapper
    {
        /// <summary>
        /// Convierte un objeto Producto a un objeto ProductoDto.
        /// </summary>
        /// <param name="producto">objeto a convertir en su parte Dto </param>
        /// <returns>devuelve el producto convertido en su parte Dto</returns>
        public static ProductoDto ToDto(this Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.Categoria?.Nombre ?? string.Empty // Manejo de null para Categoria
            };
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Producto ToEntity(this ProductoDto dto)
        {
            return new Producto
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,
                CategoriaId = dto.CategoriaId
            };
        }
    }
}
