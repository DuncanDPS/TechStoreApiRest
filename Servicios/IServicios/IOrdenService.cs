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
        /// <summary>
        /// Creates a new order based on the provided order details.
        /// </summary>
        /// <remarks>Ensure that the <paramref name="orden"/> parameter contains valid data before calling
        /// this method. This method performs validation and may throw exceptions if the input is invalid.</remarks>
        /// <param name="orden">The order details to be added. This must include all required fields for creating an order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see
        /// cref="OrdenDtoResponse"/> object with the details of the created order.</returns>
        Task<OrdenDtoResponse> CrearOrden(OrdenDtoAddRequest orden);


    }
}
