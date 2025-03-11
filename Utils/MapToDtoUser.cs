
using ACRP_API.Dtos.Users;
using ACRP_API.Models;

namespace ACRP_API.Utils
{
    public static class MapToDtoUser
    {
        public static UserResponseDto MapToDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
                NameFull = user.NameFull,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}