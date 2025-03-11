using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

 namespace ACRP_API.Dtos.Users
{

public class LoginDto
{
    [Required]
    [SwaggerSchema("Email del usuario")]
    public string Email { get; set; } = null!;

    [Required]
    [SwaggerSchema("Contraseña del usuario")]
    public string Password { get; set; } = null!;
}

}