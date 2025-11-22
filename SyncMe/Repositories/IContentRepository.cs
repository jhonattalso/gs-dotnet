using SyncMe.Models;
using SyncMe.Services; 

namespace SyncMe.Repositories {
    public interface IContentRepository {
        // O GetAll recebe os filtros para fazer a query no banco
        Task<PaginationResult<Content>> GetAllAsync(string search, int? categoryId, DifficultyLevel? difficulty, int pageIndex, int pageSize);

        Task<Content?> GetByIdAsync(int id);
        Task CreateAsync(Content content);
        Task UpdateAsync(Content content);
        Task DeleteAsync(int id);
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Track>> GetTracksAsync();
    }
}