using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;
using Servicios.IServicios;
using Servicios.DTOS;
using Servicios.DTOS.Mappers;
using System.ComponentModel.DataAnnotations;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Servicios
{
    public class ReviewService : IReviewService
    {
        private readonly AppContextDb _contextDb;
        
        public ReviewService(AppContextDb contextDb)
        {
            _contextDb = contextDb;
        }

        public async Task<ReviewDtoResponse> CrearReview(ReviewDtoAddRequest reviewDto)
        {
            if (reviewDto == null)
                throw new ArgumentNullException(nameof(reviewDto), "La Review es nula");

            // Validar los atributos de reviewDto
            var validationContext = new ValidationContext(reviewDto);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(reviewDto, validationContext, validationResults, true);

            if (!isValid)
            {
                var errores = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errores}");
            }

            // Mappeo
            Review review = ReviewMapper.DtoAddRequestToEntity(reviewDto);

            // Buscar producto
            Producto? producto = await _contextDb.Productos.FindAsync(reviewDto.ProductoId);
            if (producto == null)
                throw new KeyNotFoundException($"No se encontró el producto con ID: {reviewDto.ProductoId}");

            review.Producto = producto;

            // Buscar usuario
            Usuario? usuario = await _contextDb.Usuarios.FindAsync(reviewDto.UsuarioId);
            if (usuario == null)
                throw new KeyNotFoundException($"No se encontró el usuario con ID: {reviewDto.UsuarioId}");

            review.Usuario = usuario;

            var reviewCreada = ReviewMapper.EntityToDtoResponse(review);
            reviewCreada.NombreDeUsuario = usuario.Nombre;
            reviewCreada.NombreDeProducto = producto.Nombre;

            try
            {
                await _contextDb.Reviews.AddAsync(review);
                await _contextDb.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Puedes loggear el error aquí si tienes un logger
                throw new InvalidOperationException("Error al guardar la review en la base de datos.", ex);
            }

            return reviewCreada;
        }


        public async Task<ReviewDtoResponse> ObtenerReviewPorId(int id)
        {
            var review = await _contextDb.Reviews.FindAsync(id);
            if (review == null) throw new NullReferenceException("La Review es nula");

            return ReviewMapper.EntityToDtoResponse(review);
        }

        public async Task<IEnumerable<ReviewDtoResponse>> ObtenerTodasLasReviews()
        {
            var reviews = await _contextDb.Reviews.Select(p => p.EntityToDtoResponse() ).ToListAsync();

            if(reviews == null)
            {
                return Enumerable.Empty<ReviewDtoResponse>();
            }
            else
            {
                return reviews;
            }
        }

        public async Task<bool> EliminarReview(int id)
        {
            // buscar la review mediante el id
            Review? review = await _contextDb.Reviews.FindAsync(id);
            if (review == null) {
                return false;
                
            }
            

            _contextDb.Remove(review);
            return await _contextDb.SaveChangesAsync() > 0;
        }

        public async Task<ReviewDtoResponse> ActualizarReview(int id, ReviewDtoUpdateRequest review)
        {
            // buscar la review
            var reviewExistente = await _contextDb.Reviews.FindAsync(id);

            //validar que exista
            if(reviewExistente == null) 
            {
               throw new NullReferenceException("La review no existe");
            }

            // cambios
            reviewExistente.Titulo = review.Titulo;
            reviewExistente.Calificacion = review.Calificacion;
            reviewExistente.Comentario = review.Comentario;

            // hacer la persistencia
            await _contextDb.SaveChangesAsync();

            // pasar la entidad a response
            return ReviewMapper.EntityToDtoResponse(reviewExistente);
        }
    }
}
