using Entidades;

namespace Servicios.DTOS.Mappers
{
    public static class CategoriaMapper
    {


        /// <summary>
        /// Convierte un objeto CategoriaResponseDto a un objeto Categoria.
        /// </summary>
        /// <param name="dto">objeto dto que sera convertido a categoria</param>
        /// <returns>devuelve un objeto de tipo categoria</returns>
        public static Categoria CategoriaDtoRespToEntity(this CategoriaResponseDto dto)
        {
            return new Categoria
            {
                Id = dto.Id,
                Nombre = dto?.Nombre ?? string.Empty,
                Descripcion = dto?.Descripcion ?? string.Empty
            };
        }

        /// <summary>
        /// Convierte un objeto <see cref="CategoriaAddRequestDto"/> en una entidad <see cref="Categoria"/>.
        /// </summary>
        /// <param name="dto">El objeto de transferencia de datos que contiene los detalles de la categoría a convertir.</param>
        /// <returns>Una entidad <see cref="Categoria"/> poblada con los valores proporcionados en <paramref name="dto"/>.</returns>
        public static Categoria CategoriaAddReqToEntity(this CategoriaAddRequestDto dto) 
        {
            return new Categoria
            {
                Nombre = dto?.Nombre ?? string.Empty,
                Descripcion = dto?.Descripcion ?? string.Empty,
            };
        }


        public static CategoriaResponseDto CategoriaEntityToResponseDto(this Categoria categoria)
        {
            return new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Productos = categoria.Productos?.Select(p => ProductoMapper.EntityToDtoResponse(p)).ToList() ?? new List<ProductoResponseDto>()
            };
        }

        public static CategoriaUpdateRequestDto CategoriaDtoResponseToUpdateRequest(CategoriaResponseDto categoria)
        {
            return new CategoriaUpdateRequestDto
            {
                //Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                //Productos = categoria.Productos?
                //    .Select(ProductoMapper.ResponseDtoToEntity)
                //    .ToList()
            };
        }

        public static CategoriaResponseDto UpdateRequestToResponse(CategoriaUpdateRequestDto categoria)
        {
            return new CategoriaResponseDto
            {
                //Id = categoria.Id,
                Descripcion = categoria?.Descripcion,
                Nombre = categoria?.Nombre,
                //Productos = categoria.Productos?.Select(ProductoMapper.EntityToDtoResponse).ToList() ?? new List<ProductoResponseDto>()
            };
        }

    }
}
