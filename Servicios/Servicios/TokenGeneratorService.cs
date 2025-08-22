using Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Servicios.IServicios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Servicios.Servicios
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration _config;

        public TokenGeneratorService(IConfiguration configuration) 
        {
            _config = configuration;
        }

        public string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim (ClaimTypes.Email, usuario.Email ),
                new Claim (ClaimTypes.Role, usuario.Rol ),
                new Claim (ClaimTypes.NameIdentifier, usuario.Id.ToString() )
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );

            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
