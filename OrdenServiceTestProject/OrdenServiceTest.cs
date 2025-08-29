using Datos;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Moq;
using Servicios;
using Servicios.DTOS;
using Servicios.IServicios;
using Servicios.Servicios;
using System.Threading.Tasks;

namespace OrdenServiceTestProject
{
    public class OrdenServiceTest
    {

        [Fact]
        public async Task pruebaMetodoCrearOrden_deberiaSerExitoso()
        {
            // configura el contexto en memoria
            var options = new DbContextOptionsBuilder<AppContextDb>().UseInMemoryDatabase(databaseName: "OrdenServiceTestDb").Options;


            using var context = new AppContextDb(options);

            // Agrega un usuario de prueba al contexto
            var usuarioId = Guid.NewGuid();
            context.Usuarios.Add(new Usuario
            {
                Id = usuarioId,
                Nombre = "test",
                Apellidos = "User",
                Email = "test@correo.com",
                ContraseniaHash = "hash",
                Rol = "Cliente"
            });

            await context.SaveChangesAsync();

            // instancia el servicio real
            var ordenService = new OrdenService(context);

            // crea el request
            var addRequest = new OrdenDtoAddRequest
            {
                UsuarioId = usuarioId,
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

            // Act
            var result = await ordenService.CrearOrden(addRequest);

            // assert
            Assert.NotNull(result);
            Assert.Equal(addRequest.UsuarioId, result.UsuarioId);
            Assert.Equal(addRequest.Total, result.Total);
            Assert.Equal(addRequest.Estado, result.Estado);

        }
    }
}