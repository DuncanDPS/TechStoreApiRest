using Servicios.DTOS;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Servicios.DTOS.Mappers;
using Entidades;
using TechStoreApiRest.Servicios.DTOS.Mappers;

namespace Servicios.Servicios
{
    public class OrdenService : IOrdenService
    {
        private readonly AppContextDb _context;

        public OrdenService(AppContextDb context)
        {
            _context = context;
        }


        public async Task<OrdenDtoResponse> CrearOrden(OrdenDtoAddRequest orden)
        {
            // Validar que la orden no sea nula
            if (orden == null)
            {
                throw new ArgumentNullException(nameof(orden), "La orden no puede ser nula.");
            }
            // Validar que el usuario exista
            var usuario = _context.Usuarios.Find(orden.UsuarioId);

            if (usuario == null)
            {
                throw new ArgumentException("El usuario especificado no existe.");
            }

            // convertir Dto a entidad
            Orden ordenEntidad = OrdenMapper.DtoAddRequestToEntity(orden);

            ordenEntidad.Usuario = _context.Usuarios.Find(orden.UsuarioId);
            if(ordenEntidad.Usuario == null)
            {
                throw new ArgumentNullException();
            }
            await _context.Ordenes.AddAsync(ordenEntidad);
            await _context.SaveChangesAsync();

            return OrdenMapper.EntityToDtoResponse(ordenEntidad);
        }
    }
}
