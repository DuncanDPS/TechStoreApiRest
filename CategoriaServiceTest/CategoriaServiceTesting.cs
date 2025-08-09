//using Datos;
//using Entidades;
//using Microsoft.EntityFrameworkCore;
//using Servicios;

//namespace CategoriaServiceTest
//{
//    public class CategoriaServiceTesting
//    {
//        [Fact]
//        public void CrearCategoria_DeberiaCrearYRetornarCategoria()
//        {
//            // Arrange

//            // db context utilizando Entity Framework Core in memory
//            var option = new DbContextOptionsBuilder<AppContextDb>().UseInMemoryDatabase(databaseName: "TestDb")
//                .Options;

//            using var context = new AppContextDb(option);

//            // Creamos una instancia del servicio de categoria
//            var categoria = new Categoria
//            {
//                Nombre = "Electrónica",
//                Descripcion = "Categoría de productos electrónicos"
//            };

//            var service = new CategoriaService(context);

//            // Act
//            var resultado = service.CrearCategoria(categoria);

//            // Assert
//            Assert.NotNull(resultado); // Verificamos que el resultado no sea nulo
//            Assert.Equal(categoria.Nombre, resultado.Result.Nombre); // Verificamos que el nombre sea el mismo
//            Assert.Equal(categoria.Descripcion, resultado.Result.Descripcion); // Verificamos que la descripcion sea la misma
//            Assert.True(context.Categorias.AnyAsync(c => c.Nombre == "Electrónica").Result); // Verificamos que la categoria se haya guardado en la base de datos
            
//        }
//    }
//}