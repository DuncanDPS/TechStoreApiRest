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

        public async Task<IEnumerable<Producto>> ObtenerTodosLosProductos()
        {
            // obtenemos todos los productos de la base de datos
            return await _context.Productos.Include(p => p.Categoria).ToListAsync();
        }


        public async Task<Producto> CrearProducto(Producto producto)
        {
            // recibimos el producto y miramos si es nulo
            if (producto == null)
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

        public async Task<Producto> ActualizarProducto(Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(producto.Id); // buscar el producto por su Id

            // recibimos el producto y miramos si es nulo
            if (productoExistente == null)
            {
                throw new ArgumentNullException("Producto no encontrado.");
            }
            // actualizamos los datos del producto
            productoExistente.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Precio = producto.Precio;
            productoExistente.Stock = producto.Stock;
            productoExistente.CategoriaId = producto.CategoriaId;
            
            await _context.SaveChangesAsync(); // guardamos los cambios en la base de datos
            return productoExistente; // devolvemos el producto actualizado


        }

        public async Task<bool> EliminarProducto(Guid id)
        {
            // obtener el producto por su id
            var producto = await _context.Productos.FindAsync(id);
            // si el producto no existe, lanzamos una excepcion
            if (producto == null)
            {
                throw new ArgumentException("Producto no encontrado.");
            }
            // eliminamos el producto de la base de datos
            _context.Productos.Remove(producto);
            return await _context.SaveChangesAsync() > 0; // devolvemos true si se elimino correctamente, de lo contrario false
        }

        public async Task<Producto> ObtenerProductoPorId(Guid id)
        {
            // obtener el producto por su id
            var producto = await  _context.Productos
                .Include(p => p.Categoria) // incluir la categoria del producto
                .FirstOrDefaultAsync(p => p.Id == id); // buscar el producto por su Id

            // si el producto no existe, lanzamos una excepcion
            if (producto == null)
            {
                throw new ArgumentException("Producto no encontrado.");
            }
            return producto; // devolvemos el producto encontrado

        }


    }
}
