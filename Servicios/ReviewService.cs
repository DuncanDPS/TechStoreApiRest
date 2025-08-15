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
            if (reviewDto == null) throw new ArgumentNullException(nameof(reviewDto),"La Review nula");

            // validar los atributos de reviewDto
            var validationContext = new ValidationContext(reviewDto);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(reviewDto, validationContext, validationResults,true);

            if (!isValid)
            {
                var errores = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Errores de validacion: {errores}");
            }

            // mappeo
            Review review = ReviewMapper.DtoAddRequestToEntity(reviewDto);

            // buscamos los productos y usuarios usando su id
            var producto = await _contextDb.Productos.FindAsync(reviewDto.ProductoId);
            if (producto == null) throw new NullReferenceException("El producto es nulo");

            review.Producto = producto;

            var usuario = await _contextDb.Usuarios.FindAsync(reviewDto.UsuarioId);
            if (usuario == null) throw new NullReferenceException("El Usuario es nulo");

            review.Usuario = usuario;

            await _contextDb.Reviews.AddAsync(review);
            await _contextDb.SaveChangesAsync();
            return ReviewMapper.EntityToDtoResponse(review);
        }

        public Task<ReviewDtoResponse> ObtenerReviewPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewDtoResponse> ObtenerTodasLasReviews()
        {
            throw new NotImplementedException();
        }
    }
}
