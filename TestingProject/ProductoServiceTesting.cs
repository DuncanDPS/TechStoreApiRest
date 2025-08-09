//using Xunit;
//using Servicios;
//using Entidades;
//using Microsoft.EntityFrameworkCore;
//using Datos;


//namespace TestingProject
//{
//    public class ProductoServiceTesting
//    {
//        [Fact]
//        public async Task CrearProducto_ProductoValido_DeberiaCrearYRetornarProducto()
//        {
//            // Arrange
//            var options = new DbContextOptionsBuilder<AppContextDb>()
//                .UseInMemoryDatabase(databaseName: "TestDb").Options;
            
//            using var context = new AppContextDb(options);

//            // Agregar una categoria existente
//            var categoria = new Categoria { Nombre = "Accesorios de celular", Descripcion = "Todo tipo de accesorio para celular" };

//            context.Categorias.Add(categoria);
//            context.SaveChanges();

//            var producto = new Producto
//            {
//                Nombre = "Funda de celular",
//                Precio = 19.99m,
//                Stock = 10,
//                Descripcion = "Funda de silicona para celular",
//                CategoriaId = categoria.Id // Asignar la categoria existente
//            };

//            var service = new ProductoService(context);

//            // Act
//            var resultado = await service.CrearProducto(producto);

//            // Assert
//            Assert.NotNull(resultado);
//            Assert.Equal(producto.Nombre, resultado.Nombre);
//            Assert.Equal(producto.Precio, resultado.Precio);
//            Assert.True(context.Productos.AnyAsync(p => p.Nombre == "Funda de celular").Result);
//        }
//    }
//}