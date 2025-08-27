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
            var mockOrdenService = new Mock<IOrdenService>();

            // creamos una orden
            OrdenDtoAddRequest addRequest = new OrdenDtoAddRequest
            {
                UsuarioId = Guid.NewGuid(),
                FechaCreacion = DateTime.UtcNow,
                Total = 100.0m,
                Estado = Entidades.Enums.OrdenEstado.Pendiente,
                Items = new List<OrdenItemDtoAddRequest>
                {
                    new OrdenItemDtoAddRequest
                    {
                        ProductoId = Guid.NewGuid(),
                        Cantidad = 2,
                        PrecioUnitario = 50.0m
                    }
                }
            };

            // Configuramos el mock para que devuelva una respuesta simulada
            var expectedResponse = new OrdenDtoResponse
            {
                Id = Guid.NewGuid(),
                UsuarioId = addRequest.UsuarioId,
                FechaCreacion = addRequest.FechaCreacion,
                Total = addRequest.Total,
                Estado = addRequest.Estado,
                Items = null // Puedes simular los items si lo necesitas
            };

            mockOrdenService.Setup(s => s.CrearOrden(It.IsAny<OrdenDtoAddRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act: llamamos al metodo simulado
            var result = await mockOrdenService.Object.CrearOrden(addRequest);

            // Assert: verificar el resultado
            Assert.NotNull(result);
            Assert.Equal(addRequest.UsuarioId, result.UsuarioId);
            Assert.Equal(addRequest.Total, result.Total);
            Assert.Equal(addRequest.Estado, result.Estado);


        }
    }
}