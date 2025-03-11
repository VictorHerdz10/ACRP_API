using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Users;

public class UpdateRoleDto
{
     [SwaggerSchema("Nuevo rol del usuario")]
    public string Role { get; set; } = string.Empty;
}