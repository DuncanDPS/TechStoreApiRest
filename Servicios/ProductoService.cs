using Entidades;
using Datos;
using Microsoft.EntityFrameworkCore;
using Servicios.DTOS;
using Servicios.DTOS.Mappers;

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

        public async Task<IEnumerable<ProductoResponseDto>> ObtenerTodosLosProductos()
        {
            var listaProductos =await _context.Productos.Include(p => p.Categoria).ToListAsync();

            var listaProductoResponse = listaProductos.Select(p => p.EntityToDtoResponse()).ToList();

            return listaProductoResponse;  
        }


        public async Task<ProductoResponseDto> CrearProducto(ProductoAddRequestDto producto)
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

            // validar que la categoria exista por el nombre y asignar el Id
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nombre.ToLower() == producto.CategoriaNombre.ToLower());

            if (categoria == null)
            {
                throw new ArgumentException("La categoria especificada no existe");
            }

            // guardamos el producto en la db
            var ProductoEntidad = ProductoMapper.AddRequestToEntity(producto);
            ProductoEntidad.CategoriaId = categoria.Id; // le asignamos el id de categoria encontrado anteriormente
            await _context.Productos.AddAsync(ProductoEntidad);
            await _context.SaveChangesAsync();
            await _context.Entry(ProductoEntidad).Reference(p => p.Categoria).LoadAsync();
            return ProductoMapper.EntityToDtoResponse(ProductoEntidad);
        }

        
        public async Task<ProductoResponseDto> ActualizarProducto(ProductoUpdateRequestDto producto)
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

            await _context.SaveChangesAsync(); // guardamos los cambios en la base de datos

            return ProductoMapper.EntityToDtoResponse(productoExistente); ;  // devolvemos el producto actualizado
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

        public async Task<ProductoResponseDto> ObtenerProductoPorId(Guid id)
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
            return ProductoMapper.EntityToDtoResponse(producto); // devolvemos el producto encontrado
        }

        // TODO: Crear un metodo de servicio para obtener el producto por su nombre como opcional


    }
}
