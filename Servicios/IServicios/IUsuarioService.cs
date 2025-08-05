using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.IServicios
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Registra un nuevo usuario en el sistema utilizando la información proporcionada.
        /// </summary>
        /// <param name="usuario">El objeto usuario que contiene los datos necesarios para el registro, como nombre, apellidos, email, rol y contraseña.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado contiene el usuario registrado, incluyendo su identificador asignado.</returns>
        Task<Usuario> RegistrarUsuario(Usuario usuario);
        /// <summary>
        /// Metodo para que un usuario inicie sesion
        /// </summary>
        /// <param name="usuario">usuario que necesita iniciar sesion</param>
        /// <returns>devuelve el jwt token</returns>
        Task<Usuario> IniciarSesion(Usuario usuario);
    }
}
