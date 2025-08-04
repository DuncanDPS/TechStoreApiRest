namespace TechStoreApiRest.DTOS
{
    public class UsuarioResponse
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Rol { get; set; } = "Cliente"; // rol de cliente por defecto
    }
}
