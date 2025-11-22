using Microsoft.EntityFrameworkCore;
using SyncMe.Data;
using SyncMe.Models;
using SyncMe.Services; 

namespace SyncMe.Repositories {
    public class ContentRepository : IContentRepository {
        private readonly AppDbContext _context;

        public ContentRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<PaginationResult<Content>> GetAllAsync(string searchString, int? categoryId, DifficultyLevel? difficulty, int pageIndex, int pageSize) {
            var query = _context.Contents
                .Include(c => c.Category) 
                .Include(c => c.Track)
                .AsQueryable();

            // Filtros
            if (!string.IsNullOrEmpty(searchString)) {
                query = query.Where(c => c.Title.Contains(searchString) || c.Summary.Contains(searchString));
            }

            if (categoryId.HasValue) {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            if (difficulty.HasValue) {
                query = query.Where(c => c.Difficulty == difficulty.Value);
            }

            // Paginação
            int count = await query.CountAsync();
            var items = await query
                .OrderByDescending(c => c.PublishDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationResult<Content> {
                Items = items,
                TotalItems = count,
                CurrentPage = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<Content?> GetByIdAsync(int id) {
            return await _context.Contents
                .Include(c => c.Category)
                .Include(c => c.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(Content content) {
            _context.Add(content);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Content content) {
            _context.Update(content);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var content = await _context.Contents.FindAsync(id);
            if (content != null) {
                _context.Contents.Remove(content);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetCategoriesAsync() {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<List<Track>> GetTracksAsync() {
            return await _context.Tracks.OrderBy(t => t.Title).ToListAsync();
        }
    }
}