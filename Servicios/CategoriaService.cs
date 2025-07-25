using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.IServicios;
using Datos;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Servicios
{
    public class CategoriaService : ICategoriaService
    {
        // db context utilizando Entity Framework Core
        private readonly AppContextDb _context;

        // Constructor que recibe el contexto de la base de datos
        public CategoriaService(AppContextDb context)
        {
            _context = context;
        }

        public async Task<Categoria> ActualizarCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public async Task<Categoria> CrearCategoria(Categoria categoria)
        {
            // recibimos la categoria y miramos si es nula
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria), "La categoria no puede ser nula.");
            }
            // validamos los datos de la categoria
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                throw new ArgumentException("El nombre de la categoria no puede estar vacío.");
            }
            // validamos los data annotations
            var validationContext = new ValidationContext(categoria);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(categoria, validationContext, validationResults, true))
            {
                throw new ValidationException("Los datos de la categoria son inválidos: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            if (categoria.Id == Guid.Empty)
                categoria.Id = Guid.NewGuid();

            // guardamos la categoria en la db
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> EliminarCategoria(Guid id)
        {
            // buscamos la categoria por id
            var categoria = await _context.Categorias.FindAsync(id);
            // si no existe, devolvemos false
            if (categoria == null)
            {
                throw new KeyNotFoundException($"Categoria con ID {id} no encontrada.");
            }
            // si existe, la eliminamos
            _context.Categorias.Remove(categoria);
            return await _context.SaveChangesAsync() > 0; // devolvemos true si se elimino correctamente, de lo contrario false
        }

        public async Task<Categoria> ObtenerCategoriaPorId(Guid id)
        {
            // buscamos la categoria por id
            var categoria = await _context.Categorias.FindAsync(id);
            // si no existe, devolvemos una excepción
            if (categoria == null)
            {
                throw new KeyNotFoundException($"Categoria con ID {id} no encontrada.");
            }
            // si existe, la devolvemos
            return categoria;
        }

        public async Task<IEnumerable<Categoria>> ObtenerTodasLasCategorias()
        {
            // obtenemos todas las categorias de la base de datos
            var categorias = await _context.Categorias.ToListAsync();
            // si no hay categorias, devolvemos una lista vacía
            if (categorias == null || !categorias.Any())
            {
                return new List<Categoria>();
            }
            // si hay categorias, las devolvemos
            return categorias;
        }
    }
}
