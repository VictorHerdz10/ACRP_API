// Purpose: DTO for returning a success message.
using Swashbuckle.AspNetCore.Annotations;
namespace ACRP_API.Dtos.ResponseDto
{
    public class SuccessResponse
    {
        [SwaggerSchema("Mensaje de éxito")]
        public string Message { get; set; } = string.Empty;
        
    }
}