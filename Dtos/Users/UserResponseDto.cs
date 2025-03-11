
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Users;
public class UserResponseDto
{
    [SwaggerSchema("ID del usuario")]
    public string Id { get; set; } = string.Empty;
    [SwaggerSchema("Email del usuario")] 
    public string Email { get; set; } = null!;
    [SwaggerSchema("Nombre de usuario")]
    public string Username { get; set; } = null!;
    [SwaggerSchema("Rol del usuario")]
    public string Role { get; set; } = "user";
    [SwaggerSchema("Nombre completo del usuario")]
    public string? NameFull { get; set; }
    [SwaggerSchema("Fecha de creación del usuario")]
    public DateTime CreatedAt { get; set; }
    [SwaggerSchema("Fecha de actualización del usuario")]
    public DateTime UpdatedAt { get; set; }
}