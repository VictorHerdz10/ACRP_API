
using Swashbuckle.AspNetCore.Annotations;
namespace ACRP_API.Dtos.Users
{
    public class AuthResponse
    {
        [SwaggerSchema("Token de autenticación")]
        public string Token { get; set; } = null!;
    }
}