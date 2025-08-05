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
        private readonly ITokenGeneratorService _tokenGeneratorService;
        public UsuarioService(AppContextDb contextDb, ITokenGeneratorService tokenGeneratorService) 
        { 
            _contextDb = contextDb;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<Usuario> IniciarSesion(Usuario usuario)
        {
            // validacion de datos
            if(usuario == null) throw new ArgumentNullException(nameof(usuario), "Usuario Nulo o invalido");
            // buscar el email para comprobar que exista y comparar la contrasenia
            var usuarioExistente = await _contextDb.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);
            if (usuarioExistente.Email != usuario.Email) throw new ArgumentException("El email del usuario no coincide");
            // verificar las contrasenias
            if (BCrypt.Net.BCrypt.Verify(usuario.ContraseniaHash, usuarioExistente!.ContraseniaHash))
            {
                return usuario;
            }
            else
            {
                throw new ArgumentException("Contrasenia Invalida");
            }
             
            
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

     
       
    }
}
