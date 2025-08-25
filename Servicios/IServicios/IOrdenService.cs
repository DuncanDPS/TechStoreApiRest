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
    /// Esta interfaz define los métodos para manejar operaciones relacionadas con órdenes.
    /// </summary>
    public interface IOrdenService
    {
        // crear orden
        Task<OrdenDtoResponse> CrearOrden(OrdenDtoAddRequest orden);
    }
}
