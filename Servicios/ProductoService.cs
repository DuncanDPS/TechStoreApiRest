using Entidades;
using Datos;
using Microsoft.EntityFrameworkCore;
namespace Servicios
{
    public class ProductoService : IServicios.IProductoService
    {
        // data context utilizando Entity Framework Core
        private readonly AppContextDb  _context;

        // Constructor que recibe el contexto de la base de datos
        public ProductoService(AppContextDb context)
        {
            _context = context;
        }

        public Task<Producto> ActualizarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        // TODO: Hacer el metodo y luego la prueba unitaria
        public async Task<Producto> CrearProducto(Producto producto)
        {
            // recibimos el producto y miramos si es nulo
            if(producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo.");   
            }
            // validamos los datos del producto
            if (string.IsNullOrWhiteSpace(producto.Nombre) || producto.Precio <= 0 || producto.Stock < 0)
            {
                throw new ArgumentException("Los datos del producto son inválidos.");
            }

            // validar que la categoria exista
            var categoriaExistente = await _context.Categorias.AnyAsync(c => c.Id == producto.CategoriaId);
            if (!categoriaExistente)
            {
                throw new ArgumentException("La categoria Especificada no existe");
            }

            // guardamos el producto en la db
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
            return producto;

        }

        public Task<bool> EliminarProducto(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Producto> ObtenerProductoPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Producto>> ObtenerTodosLosProductos()
        {
            throw new NotImplementedException();
        }
    }
}
