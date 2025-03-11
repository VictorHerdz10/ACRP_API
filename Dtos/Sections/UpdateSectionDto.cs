using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Sections
{
    public class UpdateSectionDto
    {
        [SwaggerSchema("El título de la sección")]
        public string? Title { get; set; }

        [SwaggerSchema("La descripción de la sección")]
        public string? Description { get; set; }
    }
}