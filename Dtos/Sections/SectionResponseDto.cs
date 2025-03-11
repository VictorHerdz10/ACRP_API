using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Sections
{
    public class SectionResponseDto
    {
        [SwaggerSchema("El identificador único de la sección")]
        public string Id { get; set; } = string.Empty;

        [SwaggerSchema("El título de la sección")]
        public string Title { get; set; } = null!;

        [SwaggerSchema("La descripción de la sección")]
        public string Description { get; set; } = null!;

        [SwaggerSchema("Fecha de creación")]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema("Fecha de última actualización")]
        public DateTime UpdatedAt { get; set; }
    }
}