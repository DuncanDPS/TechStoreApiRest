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
                
            };
        }
    }
}