using Servicios.DTOS;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Servicios.Servicios
{
    public class OrdenService : IOrdenService
    {
        private readonly AppContextDb _context;

        public OrdenService(AppContextDb context)
        {
            _context = context;
        }


        public Task<OrdenDtoResponse> CrearOrden(OrdenDtoAddRequest orden)
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

            //
            
            
            throw new NotImplementedException();
        }
    }
}
