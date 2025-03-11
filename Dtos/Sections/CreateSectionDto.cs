using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Dtos.Sections
{
    public class CreateSectionDto
    {
        [Required]
        [SwaggerSchema("El título de la sección")]
        public string Title { get; set; } = null!;

        [Required]
        [SwaggerSchema("La descripción de la sección")]
        public string Description { get; set; } = null!;
    }
}