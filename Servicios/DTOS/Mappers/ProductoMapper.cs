using Entidades;
using Servicios.DTOS;
using System.Runtime.CompilerServices;

namespace Servicios.DTOS.Mappers
{
    public static class ProductoMapper
    {
        /// <summary>
        /// Convierte un objeto Producto a un objeto ProductoResponseDto.
        /// </summary>
        /// <param name="producto">objeto a convertir en su parte Dto </param>
        /// <returns>devuelve el producto convertido en su parte Dto</returns>
        public static ProductoResponseDto EntityToDtoResponse(this Producto producto)
        {
            return new ProductoResponseDto
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
        /// Convierte un objeto <see cref="ProductoAddRequestDto"/> en una entidad <see cref="Producto"/>.
        /// </summary>
        /// <param name="productoDto">El DTO con los datos necesarios para crear un producto.</param>
        /// <returns>Una nueva instancia de <see cref="Producto"/> con los valores asignados desde el DTO.</returns>
        public static Producto AddRequestToEntity(ProductoAddRequestDto productoDto)
        {

            return new Producto
            {
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock,
                //CategoriaId = productoDto.CategoriaNombre ?? string.Empty
            };

        }

        /// <summary>
        /// Convierte un objeto <see cref="ProductoUpdateRequestDto"/> en una entidad <see cref="Producto"/>.
        /// </summary>
        /// <param name="productoDto">
        /// El DTO con los datos necesarios para actualizar un producto.
        /// </param>
        /// <returns>
        /// Una nueva instancia de <see cref="Producto"/> con los valores asignados desde el DTO de actualización.
        /// </returns>
        public static Producto UpdateRequestToEntity(this ProductoUpdateRequestDto productoDto)
        {

            return new Producto
            {
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock,
                //CategoriaId = productoDto.CategoriaNombre ?? string.Empty
            };

        }

        /// <summary>
        /// Mapea un Producto en ProductoUpdateRequest
        /// </summary>
        /// <param name="producto">producto especificado para el mapeo</param>
        /// <returns>devuelve un ProductoUpdateRequestDto</returns>
        public static ProductoUpdateRequestDto EntityToUpdateReq(this Producto producto)
        {
            return new ProductoUpdateRequestDto
            {
                //Id = producto.Id,
                CategoriaNombre = producto.Categoria?.Nombre ?? string.Empty,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Nombre = producto.Nombre
            };
        }

        /// <summary>
        /// Mapea un <see cref="ProductoResponseDto"/> a un <see cref="ProductoUpdateRequestDto"/>.
        /// </summary>
        /// <param name="productoDto">El DTO de respuesta del producto a mapear.</param>
        /// <returns>Un nuevo <see cref="ProductoUpdateRequestDto"/> con los valores del DTO de respuesta.</returns>
        public static ProductoUpdateRequestDto ResponseDtoToUpdateRequest(this ProductoResponseDto productoDto)
        {
            return new ProductoUpdateRequestDto
            {
                //Id = productoDto.Id,
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock,
                CategoriaNombre = productoDto.CategoriaNombre
            };
        }

        /// <summary>
        /// Dto response a entidad
        /// </summary>
        /// <param name="dto">Response Dto que sera convertido en entidad</param>
        /// <returns>Una entidad Producto</returns>
        public static Producto ResponseDtoToEntity(ProductoResponseDto dto)
        {
            return new Producto
            {
                Id = dto.Id,
                Nombre = dto.Nombre ?? string.Empty,
                Descripcion = dto.Descripcion ?? string.Empty,
                Precio = dto.Precio,
                Stock = dto.Stock,
                CategoriaId = dto.CategoriaId
            };
        }

       

    }
}
