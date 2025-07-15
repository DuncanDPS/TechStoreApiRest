using Entidades;
using Datos;
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

        public Task<Producto> CrearProducto(Producto producto)
        {
            throw new NotImplementedException();
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
