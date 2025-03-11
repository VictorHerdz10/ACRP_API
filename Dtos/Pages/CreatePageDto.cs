using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Pages
{
    public class CreatePageDto
    {
        [Required]
        [SwaggerSchema("El nombre de la página")]
        public string Name { get; set; } = null!;

        [Required]
        [SwaggerSchema("La descripción de la página")]
        public string Description { get; set; } = null!;

        [Required]
        [SwaggerSchema("El identificador de la seccion enlazada a esta página")]
        public string SectionId { get; set; } = null!;

        [Required]
        [SwaggerSchema("La categoría de la página")]
        public string Category { get; set; } = null!;

        [SwaggerSchema("Atributos específicos de la página en formato JSON")]
        public string SpecificAttributes { get; set; } = "{}";
    }
}