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
using Servicios.DTOS;
using Servicios.DTOS.Mappers;

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

        // TODO: Implementar metodo para actualizar Cat
        public async Task<CategoriaResponseDto> ActualizarCategoria(Guid id, CategoriaUpdateRequestDto categoriaUpdateReq)
        {
            if (categoriaUpdateReq == null) throw new ArgumentNullException(nameof(categoriaUpdateReq), "La categoria no puede ser nula");

            var categoriaExiste = await _context.Categorias.FindAsync(id);

            if (categoriaExiste == null) throw new ArgumentNullException(nameof(categoriaExiste), "Categoria No Encontrada");

            // si no se cumple el if anterior, se procede a actualizar datos
            categoriaExiste.Nombre = categoriaUpdateReq.Nombre;
            categoriaExiste.Descripcion = categoriaUpdateReq.Descripcion;
            //categoriaExiste.Productos = categoriaUpdateReq.Productos;

            // guardar cambios
            await _context.SaveChangesAsync();
            return CategoriaMapper.CategoriaEntityToResponseDto(categoriaExiste);
        }

        public async Task<CategoriaResponseDto> CrearCategoria(CategoriaAddRequestDto categoria)
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

            Categoria categoriaEntity = CategoriaMapper.CategoriaAddReqToEntity(categoria);
            await _context.Categorias.AddAsync(categoriaEntity);
            await _context.SaveChangesAsync();
            return CategoriaMapper.CategoriaEntityToResponseDto(categoriaEntity);
        }



        public async Task<CategoriaResponseDto> ObtenerCategoriaPorId(Guid id)
        {
            // buscamos la categoria por id
            var categoria = await _context.Categorias.FindAsync(id);
            // si no existe, devolvemos una excepción
            if (categoria == null)
            {
                throw new KeyNotFoundException($"Categoria con ID {id} no encontrada.");
            }

            // si existe, la devolvemos
            return CategoriaMapper.CategoriaEntityToResponseDto(categoria);
        }

        public async Task<IEnumerable<CategoriaResponseDto>> ObtenerTodasLasCategorias()
        {
            // obtenemos todas las categorias de la base de datos
            var categorias = await _context.Categorias.Include(c => c.Productos).ToListAsync();
            // si no hay categorias, devolvemos una lista vacía
            if (categorias == null || !categorias.Any())
            {
                throw new KeyNotFoundException("No existen Categorias");
            }
            var categoriaResponse = categorias.Select(c => c.CategoriaEntityToResponseDto()).ToList();
            return categoriaResponse;
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


    }
}
