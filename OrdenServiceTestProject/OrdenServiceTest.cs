using Entidades;
using Servicios;
using Servicios.IServicios;
using Servicios.DTOS;
using Moq;
using System.Threading.Tasks;

namespace OrdenServiceTestProject
{
    public class OrdenServiceTest
    {
        [Fact]
        public async Task pruebaMetodoCrearOrden_deberiaSerExitoso()
        {
            // Mock de dependencias
            var mockRepo = new Mock<IOrdenService>
        }
    }
}