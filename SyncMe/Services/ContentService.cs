using SyncMe.Models;
using SyncMe.Repositories; 

namespace SyncMe.Services {
    public class PaginationResult<T> {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }

    public class ContentService {
        private readonly IContentRepository _repository; 

        public ContentService(IContentRepository repository) {
            _repository = repository;
        }

        public async Task<PaginationResult<Content>> GetAllAsync(string search, int? catId, DifficultyLevel? diff, int page, int size) {
            return await _repository.GetAllAsync(search, catId, diff, page, size);
        }

        public async Task<Content?> GetByIdAsync(int id) {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Content content) {
            if (content.PublishDate > DateTime.Now.AddDays(30)) {
                throw new Exception("Não é permitido agendar conteúdo com mais de 30 dias de antecedência.");
            }
            await _repository.CreateAsync(content);
        }

        public async Task UpdateAsync(Content content) {
            await _repository.UpdateAsync(content);
        }

        public async Task DeleteAsync(int id) {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<Category>> GetCategoriesAsync() {
            return await _repository.GetCategoriesAsync();
        }

        public async Task<List<Track>> GetTracksAsync() {
            return await _repository.GetTracksAsync();
        }
    }
}