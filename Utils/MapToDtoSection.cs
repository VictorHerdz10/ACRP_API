
using ACRP_API.Dtos.Sections;
using ACRP_API.Models;

namespace ACRP_API.Utils
{
    public static class MapToDtoSection
    {
        public static SectionResponseDto MapToDto(Section section)
        {
            return new SectionResponseDto
            {
                Id = section.Id,
                Title = section.Title,
                Description = section.Description,
                CreatedAt = section.CreatedAt,
                UpdatedAt = section.UpdatedAt
            };
        }
    }
}