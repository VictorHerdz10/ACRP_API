using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ACRP_API.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [SwaggerSchema("El identificador único del usuario")]
    public string Id { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [SwaggerSchema("La dirección de correo electrónico del usuario")]
    public string Email { get; set; } = null!;

    [Required]
    [SwaggerSchema("El nombre de usuario")]
    public string Username { get; set; } = null!;

    [Required]
    [MinLength(6)]
    [SwaggerSchema("La contraseña del usuario")]
    public string Password { get; set; } = null!;

    [SwaggerSchema("El rol del usuario")]
    public string Role { get; set; } = " ";

    [SwaggerSchema("El nombre completo del usuario")]
    public string NameFull { get; set; } = null!;

    [BsonDateTimeOptions(Representation = BsonType.DateTime)]
    [SwaggerSchema("La fecha de creación del usuario")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [BsonDateTimeOptions(Representation = BsonType.DateTime)]
    [SwaggerSchema("La fecha de actualización del usuario")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [BsonIgnore]
    [SwaggerSchema("La versión del documento")]
    public int? __v { get; set; }
}