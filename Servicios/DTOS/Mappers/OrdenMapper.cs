using Datos;
using Entidades;
using Servicios.DTOS;
using TechStoreApiRest.Servicios.DTOS;

namespace TechStoreApiRest.Servicios.DTOS.Mappers
{
    public static class OrdenMapper
    {
        

        // DtoAddRequest to Entity
        public static Orden DtoAddRequestToEntity(OrdenDtoAddRequest orden)
        {
            return new Orden
            {
                UsuarioId = orden.UsuarioId,
                Estado = orden.Estado,
                Total = orden.Total,
                Items = orden.Items?.Select(itemDto => new OrdenItem
                {
                    ProductoId = itemDto.ProductoId,
                    Cantidad = itemDto.Cantidad,
                    PrecioUnitario = itemDto.PrecioUnitario,
                }).ToList()
            };
        }


        // entity to DtoResponse
        public static OrdenDtoResponse EntityToDtoResponse(Orden orden)
        {
            return new OrdenDtoResponse
            {
                Id = orden.Id,
                Estado = orden.Estado,
                FechaCreacion = orden.FechaCreacion,
                Total = orden.Total,
                 Usuario = orden.Usuario,
                 UsuarioId = orden.UsuarioId,
                 Items = orden.Items?.Select(item => new OrdenItemDtoResponse
                 {
                      Id = item.Id,
                      Cantidad = item.Cantidad,
                      OrdenId = item.OrdenId,
                      PrecioUnitario = item.PrecioUnitario,
                      ProductoId = item.ProductoId,
                      ProductoNombre = item.Producto?.Nombre
                 }).ToList()
            };
        }

    }
}