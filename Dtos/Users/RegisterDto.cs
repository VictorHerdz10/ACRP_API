using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Users
{
public class RegisterDto
{
    [Required]
    [EmailAddress]
     [SwaggerSchema("Email del usuario")]
    public string Email { get; set; } = null!;

    [Required]
    [SwaggerSchema("Nombre de usuario")]
    public string Username { get; set; } = null!;

    [Required]
    [MinLength(6)]
    [SwaggerSchema("Contraseña del usuario")]
    public string Password { get; set; } = null!;
}
}