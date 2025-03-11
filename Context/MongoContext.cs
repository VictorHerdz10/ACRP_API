using MongoDB.Driver;
using ACRP_API.Models;

namespace ACRP_API.Context;
public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly ILogger<MongoDbContext> _logger;
    public MongoDbContext(string ConnectionString, string DatabaseName, ILogger<MongoDbContext> logger)
    {
        _logger = logger;
        var client = new MongoClient(ConnectionString);
        _database = client.GetDatabase(DatabaseName);

        try
        {
            client.Cluster.Description.ToString();
            _logger.LogInformation("Conexión a MongoDB establecida correctamente."); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al conectar a MongoDB.");
            throw new InvalidOperationException("No se pudo conectar a MongoDB.", ex);
        }
    }

    // Colecciones existentes
    public IMongoCollection<User> Usuarios =>
        _database.GetCollection<User>("users");

    public IMongoCollection<Section> Secciones =>
        _database.GetCollection<Section>("sections");

    public IMongoCollection<Page> Paginas =>
        _database.GetCollection<Page>("pages");
}

