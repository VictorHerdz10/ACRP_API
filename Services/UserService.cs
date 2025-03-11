using ACRP_API.Models;
using MongoDB.Driver;
using ACRP_API.Context;
using ACRP_API.Dtos.Users;
using ACRP_API.Utils;

namespace ACRP_API.Services
{
    public interface IUserService
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(string id);
        Task<User> CreateAsync(User newUser);
        Task UpdateRoleAsync(string id, string role);
        Task RemoveAsync(string id);
        bool ValidatePassword(User user, string password);
        Task<string> GetRoleAsync(string userId);
        Task<List<UserResponseDto>> GetAllUsersAsync();
    }

    public class UsersService(MongoDbContext context) : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection = context.Usuarios;

        public async Task<User?> GetByEmailAsync(string email) =>
            await _usersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task<User?> GetByIdAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<User> CreateAsync(User newUser)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            await _usersCollection.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task UpdateRoleAsync(string id, string role)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            var update = Builders<User>.Update.Set(x => x.Role, role);
            await _usersCollection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);

        public bool ValidatePassword(User user, string password) =>
            BCrypt.Net.BCrypt.Verify(password, user.Password);

        public async Task<string> GetRoleAsync(string userId)
        {
            var user = await _usersCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();
            return user.Role;
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _usersCollection.Find(_ => true).ToListAsync();
            return [.. users.Select(MapToDtoUser.MapToDto)];
        }
    }
}