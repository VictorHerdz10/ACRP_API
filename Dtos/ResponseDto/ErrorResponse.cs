// Purpose: This file contains the ErrorResponse class used to return error messages to the client.
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.ResponseDto
{
    public class ErrorResponse
    {
        [SwaggerSchema("Mensaje de error")]
        public string Message { get; set; } = string.Empty;
    }
}