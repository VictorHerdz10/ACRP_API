using ACRP_API.Models;
using MongoDB.Driver;
using ACRP_API.Context;
using ACRP_API.Dtos.Sections;
using ACRP_API.Utils;

namespace ACRP_API.Services
{
    public interface ISectionService
    {
        Task<List<SectionResponseDto>> GetAllSectionsAsync();
        Task<SectionResponseDto?> GetSectionByIdAsync(string id);
        Task<Section> CreateSectionAsync(CreateSectionDto sectionDto);
        Task UpdateSectionAsync(string id, UpdateSectionDto sectionDto);
        Task DeleteSectionAsync(string id);
        Task<bool> SectionTitleExistsAsync(string title);
    }

    public class SectionService(MongoDbContext context) : ISectionService
    {
        private readonly IMongoCollection<Section> _sectionsCollection = context.Secciones;

        public async Task<List<SectionResponseDto>> GetAllSectionsAsync()
        {
            var sections = await _sectionsCollection.Find(_ => true).ToListAsync();
            return sections.Select(MapToDtoSection.MapToDto).ToList();
        }

        public async Task<SectionResponseDto?> GetSectionByIdAsync(string id)
        {
            var section = await _sectionsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
            return section != null ? MapToDtoSection.MapToDto(section) : null;
        }

        public async Task<Section> CreateSectionAsync(CreateSectionDto sectionDto)
        {
            var section = new Section
            {
                Title = sectionDto.Title,
                Description = sectionDto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _sectionsCollection.InsertOneAsync(section);
            return section;
        }

        public async Task UpdateSectionAsync(string id, UpdateSectionDto sectionDto)
        {
            var update = Builders<Section>.Update;
            var updates = new List<UpdateDefinition<Section>>();

            if (sectionDto.Title != null)
                updates.Add(update.Set(s => s.Title, sectionDto.Title));
            if (sectionDto.Description != null)
                updates.Add(update.Set(s => s.Description, sectionDto.Description));

            updates.Add(update.Set(s => s.UpdatedAt, DateTime.UtcNow));

            await _sectionsCollection.UpdateOneAsync(
                s => s.Id == id,
                update.Combine(updates)
            );
        }

        public async Task DeleteSectionAsync(string id)
        {
            await _sectionsCollection.DeleteOneAsync(s => s.Id == id);
        }

        public async Task<bool> SectionTitleExistsAsync(string title)
        {
            var normalizedTitle = StringUtils.NormalizeString(title);
            var sections = await _sectionsCollection.Find(_ => true).ToListAsync();
            return sections.Any(s => StringUtils.NormalizeString(s.Title) == normalizedTitle);
        }
    }
}