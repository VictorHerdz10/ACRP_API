
using ACRP_API.Dtos.Pages;
using ACRP_API.Models;

namespace ACRP_API.Utils
{
    public static class MapToDtoPage
    {
        public static PageResponseDto MapToDto(Page page)
        {
            return new PageResponseDto
            {
                Id = page.Id,
                Name = page.Name,
                Description = page.Description,
                SectionId = page.SectionId,
                Category = page.Category,
                SpecificAttributes = page.SpecificAttributes,
                CreatedAt = page.CreatedAt,
                UpdatedAt = page.UpdatedAt
            };
        }
    }
}