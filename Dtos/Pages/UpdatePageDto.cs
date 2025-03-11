using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Pages
{
    public class UpdatePageDto
    {
        [SwaggerSchema("El nombre de la página")]
        public string? Name { get; set; }

        [SwaggerSchema("La descripción de la página")]
        public string? Description { get; set; }

        [SwaggerSchema("El identificador de la sección enlazada a esta página")]
        public string? SectionId { get; set; }

        [SwaggerSchema("La categoría de la página a la que pertenece")]
        public string? Category { get; set; }

        [SwaggerSchema("Atributos específicos de la página en formato JSON")]
        public string? SpecificAttributes { get; set; }
    }
}