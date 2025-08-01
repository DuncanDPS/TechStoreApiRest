using Entidades;

namespace Servicios.IServicios
{
    /// <summary>
    /// Define un servicio para generar tokens de autenticación para usuarios.
    /// </summary>
    /// <remarks>Esta interfaz proporciona un método para generar un token basado en la información del usuario proporcionada.
    /// Las implementaciones de esta interfaz deben asegurar que los tokens generados sean seguros y adecuados para los
    /// fines de autenticación o autorización previstos.</remarks>
    public interface ITokenGeneratorService
    {
        /// <summary>
        /// Genera un JSON Web Token (JWT) para el usuario especificado.
        /// </summary>
        /// <remarks>El token generado puede ser utilizado para propósitos de autenticación y autorización.
        /// Asegúrese de que el objeto usuario contenga la información necesaria requerida para la generación del token.</remarks>
        /// <param name="usuario">El usuario para quien se generará el token. Este parámetro no puede ser nulo.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene el JWT generado como una cadena.</returns>
        string GenerarToken(Usuario usuario); 
    }
}
