
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Pages
{
    public class PageResponseDto
    {
        [SwaggerSchema("El identificador único de la página")]
        public string Id { get; set; } = string.Empty;

        [SwaggerSchema("El nombre de la página")]
        public string Name { get; set; } = null!;

        [SwaggerSchema("La descripción de la página")]
        public string Description { get; set; } = null!;

        [SwaggerSchema("La categoría de la página")]
        public string Category { get; set; } = null!;

        [SwaggerSchema("El identificador de la seccion enlazada a esta página")]
        public string SectionId { get; set; } = null!;

        [SwaggerSchema("Atributos específicos de la página en formato JSON")]
        public string SpecificAttributes { get; set; } = "{}";

        [SwaggerSchema("Fecha de creación")]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema("Fecha de última actualización")]
        public DateTime UpdatedAt { get; set; }
    }
}