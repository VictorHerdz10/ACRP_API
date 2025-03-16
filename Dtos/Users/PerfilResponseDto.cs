using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Users
{
    public class PerfilResponseDto
    {
        [SwaggerSchema("Email del usuario")]
        public string Email { get; set; } = null!;

        [SwaggerSchema("Nombre de usuario")]
        public string Username { get; set; } = null!;

        [SwaggerSchema("Nombre completo del usuario")]
        public string? NameFull { get; set; }
    }
}