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
        public static ProductoResponseDto ToDtoResponse(this Producto producto)
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
        /// Convierte un objeto <see cref="ProductoUpdateRequest"/> en una entidad <see cref="Producto"/>.
        /// </summary>
        /// <param name="productoDto">
        /// El DTO con los datos necesarios para actualizar un producto.
        /// </param>
        /// <returns>
        /// Una nueva instancia de <see cref="Producto"/> con los valores asignados desde el DTO de actualización.
        /// </returns>
        public static Producto UpdateRequestToEntity(ProductoUpdateRequest productoDto)
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


    }
}
