
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Models
{
    public class Page
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [SwaggerSchema("El identificador único de la página")]
        public string Id { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("El nombre de la página")]
        public string Name { get; set; } = null!;

        [Required]
        [SwaggerSchema("La descripción de la página")]
        public string Description { get; set; } = null!;

        [SwaggerSchema("Atributos específicos de la página en formato JSON")]
        public string SpecificAttributes { get; set; } = "{}";
        
        [Required]
        [SwaggerSchema("El ID de la sección a la que asociada a esta pagina")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SectionId { get; set; } = null!;

        [Required]
        [SwaggerSchema("La categoría de la página")]
        public string Category { get; set; } = null!;

        [BsonDateTimeOptions(Representation = BsonType.DateTime)]
        [SwaggerSchema("La fecha de creación de la página")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonDateTimeOptions(Representation = BsonType.DateTime)]
        [SwaggerSchema("La fecha de actualización de la página")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [BsonIgnore]
        [SwaggerSchema("La versión del documento")]
        public int? __v { get; set; }
    }
}