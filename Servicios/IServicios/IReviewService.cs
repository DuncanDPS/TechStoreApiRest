using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.DTOS;

namespace Servicios.IServicios
{
    /// <summary>
    /// Defines the contract for managing reviews within the application.
    /// </summary>
    /// <remarks>This interface provides methods for creating and managing reviews. Implementations of this
    /// interface should handle the business logic and data persistence for review-related operations.</remarks>
    public interface IReviewService
    {
        /// <summary>
        /// Creates a new review based on the provided data.
        /// </summary>
        /// <remarks>This method performs validation on the input data before creating the review. Ensure
        /// that  all required fields in <paramref name="reviewDto"/> are populated and meet the expected 
        /// constraints.</remarks>
        /// <param name="reviewDto">The data for the review to be created. Must include all required fields for a valid review.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a  <see
        /// cref="ReviewDtoResponse"/> object with the details of the created review.</returns>
        Task<ReviewDtoResponse> CrearReview(ReviewDtoAddRequest reviewDto);

        /// <summary>
        /// Retrieves all reviews from the system.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch all reviews. The returned 
        /// <see cref="ReviewDtoResponse"/> includes the collection of reviews and any associated metadata.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a  <see cref="ReviewDtoResponse"/>
        /// object with the details of all reviews.</returns>
        Task<ReviewDtoResponse> ObtenerTodasLasReviews();

        /// <summary>
        /// Retrieves the review associated with the specified identifier.
        /// </summary>
        /// <remarks>Use this method to fetch detailed information about a specific review. Ensure the
        /// <paramref name="id"/> parameter is valid and corresponds to an existing review.</remarks>
        /// <param name="id">The unique identifier of the review to retrieve. Must be a positive integer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see
        /// cref="ReviewDtoResponse"/> object representing the review details, or <see langword="null"/> if no review is
        /// found for the specified identifier.</returns>
        Task<ReviewDtoResponse> ObtenerReviewPorId(int id);


    }


}
