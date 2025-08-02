using Datos;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppContextDb _contextDb;
        public UsuarioService(AppContextDb contextDb) 
        { 
            _contextDb = contextDb;
        }

        public async Task<Usuario> RegistrarUsuario(Usuario usuario)
        {
            // validacion de datos
            if(usuario == null) throw new ArgumentNullException(nameof(usuario), "Usuario Nulo o invalido");
            
            if(await _contextDb.Usuarios.AnyAsync(u => u.Email == usuario.Email))
            {
                throw new InvalidOperationException($"El email ya existe: {usuario.Email}, porfavor registre uno diferente");
            }

            usuario.ContraseniaHash = BCrypt.Net.BCrypt.HashPassword(usuario.ContraseniaHash);

            // se guarda el usuario en la DB
            _contextDb.Usuarios.Add(usuario);
            await _contextDb.SaveChangesAsync();
            return usuario;
        }

        // TODO: LOGIN QUE LLAMA A EL SERVICIO QUE GENERA EL TOKEN
    }
}
