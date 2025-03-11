using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Models
{
    public class Section
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [SwaggerSchema("El identificador único de la sección")]
        public string Id { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("El título de la sección")]
        public string Title { get; set; } = null!;

        [Required]
        [SwaggerSchema("La descripción de la sección")]
        public string Description { get; set; } = null!;

        [BsonDateTimeOptions(Representation = BsonType.DateTime)]
        [SwaggerSchema("La fecha de creación de la sección")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonDateTimeOptions(Representation = BsonType.DateTime)]
        [SwaggerSchema("La fecha de actualización de la sección")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [BsonIgnore]
        [SwaggerSchema("La versión del documento")]
        public int? __v { get; set; }
    }
}