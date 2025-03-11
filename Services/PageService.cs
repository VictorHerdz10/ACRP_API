using ACRP_API.Models;
using MongoDB.Driver;
using ACRP_API.Context;
using ACRP_API.Dtos.Pages;
using ACRP_API.Utils;

namespace ACRP_API.Services
{
    public interface IPageService
    {
        Task<List<PageResponseDto>> GetAllPagesAsync();
        Task<PageResponseDto?> GetPageByIdAsync(string id);
        Task<Page> CreatePageAsync(CreatePageDto pageDto);
        Task UpdatePageAsync(string id, UpdatePageDto pageDto);
        Task DeletePageAsync(string id);
        Task<bool> SectionExistsAsync(string sectionId);
        Task<bool> PageNameExistsAsync(string name);
    }

    public class PageService(MongoDbContext context) : IPageService
    {
        private readonly IMongoCollection<Page> _pagesCollection = context.Paginas;
        private readonly IMongoCollection<Section> _sectionsCollection = context.Secciones;

        public async Task<List<PageResponseDto>> GetAllPagesAsync()
        {
            var pages = await _pagesCollection.Find(_ => true).ToListAsync();
            return pages.Select(MapToDtoPage.MapToDto).ToList();
        }

        public async Task<PageResponseDto?> GetPageByIdAsync(string id)
        {
            var page = await _pagesCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
            return page != null ? MapToDtoPage.MapToDto(page) : null;
        }

        public async Task<Page> CreatePageAsync(CreatePageDto pageDto)
        {
            var page = new Page
            {
                Name = pageDto.Name,
                Description = pageDto.Description,
                SectionId = pageDto.SectionId,
                Category = pageDto.Category,
                SpecificAttributes = pageDto.SpecificAttributes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _pagesCollection.InsertOneAsync(page);
            return page;
        }

        public async Task UpdatePageAsync(string id, UpdatePageDto pageDto)
        {
            var update = Builders<Page>.Update;
            var updates = new List<UpdateDefinition<Page>>();

            if (pageDto.Name != null)
                updates.Add(update.Set(p => p.Name, pageDto.Name));
            if (pageDto.Description != null)
                updates.Add(update.Set(p => p.Description, pageDto.Description));
            if (pageDto.SectionId != null)
                updates.Add(update.Set(p => p.SectionId, pageDto.SectionId));
            if (pageDto.Category != null)
                updates.Add(update.Set(p => p.Category, pageDto.Category));
            if (pageDto.SpecificAttributes != null)
                updates.Add(update.Set(p => p.SpecificAttributes, pageDto.SpecificAttributes));

            updates.Add(update.Set(p => p.UpdatedAt, DateTime.UtcNow));

            await _pagesCollection.UpdateOneAsync(
                p => p.Id == id,
                update.Combine(updates)
            );
        }

        public async Task DeletePageAsync(string id)
        {
            await _pagesCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<bool> SectionExistsAsync(string sectionId)
        {
            var section = await _sectionsCollection.Find(s => s.Id == sectionId).FirstOrDefaultAsync();
            return section != null;
        }
        public async Task<bool> PageNameExistsAsync(string name)
        {
            var normalizedName = StringUtils.NormalizeString(name);
            var pages = await _pagesCollection.Find(_ => true).ToListAsync();
            return pages.Any(p => StringUtils.NormalizeString(p.Name) == normalizedName);
        }
    }
}