using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Servicios.DTOS;
using Entidades;
using System.Diagnostics;
using Datos;

namespace Servicios.DTOS.Mappers
{
    public static class ReviewMapper
    {
  

        // ReviewDtoAddRequest TO Entity
        public static Review DtoAddRequestToEntity(this ReviewDtoAddRequest review)
        {
            return new Review
            {
                Titulo = review.Titulo,
                Calificacion = review.Calificacion,
                Comentario = review.Comentario,
                ProductoId = review.ProductoId,
                UsuarioId = review.UsuarioId,
            };
        }

        // Entity to ReviewDtoResponse
        public static ReviewDtoResponse EntityToDtoResponse(this Review review)
        {
            return new ReviewDtoResponse
            {
                Titulo = review.Titulo,
                Calificacion = review.Calificacion,
                Comentario = review.Comentario,
                FechaCreacion = review.FechaCreacion,
                Id = review.Id,
                ProductoId = review.ProductoId,
                UsuarioId = review.UsuarioId
                
            };
        }

    }
}
