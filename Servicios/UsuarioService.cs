using Datos;
using Entidades;
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

        public Task<Usuario> RegistrarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
