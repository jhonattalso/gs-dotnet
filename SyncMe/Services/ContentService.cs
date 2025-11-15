using Microsoft.EntityFrameworkCore;
using SyncMe.Data;
using SyncMe.Models;

namespace SyncMe.Services {
    public class ContentService {
        private readonly AppDbContext _context;

        public ContentService(AppDbContext context) {
            _context = context;
        }

        // Atualizado: Agora aceita Filtros de Categoria e Dificuldade
        public async Task<PaginationResult> GetAllAsync(
            string searchString,
            int? categoryId,
            DifficultyLevel? difficulty,
            int pageIndex = 1,
            int pageSize = 10) {
            // Include: Traz os dados da Categoria junto (JOIN)
            var query = _context.Contents
                .Include(c => c.Category)
                .Include(c => c.Track)
                .AsQueryable();

            // Filtro por Texto (Título)
            if (!string.IsNullOrEmpty(searchString)) {
                query = query.Where(c => c.Title.Contains(searchString) || c.Summary.Contains(searchString));
            }

            // Filtro por Categoria (Dropdown)
            if (categoryId.HasValue) {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            // Filtro por Dificuldade
            if (difficulty.HasValue) {
                query = query.Where(c => c.Difficulty == difficulty.Value);
            }

            query = query.OrderByDescending(c => c.PublishDate);

            int count = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginationResult {
                Items = items,
                TotalItems = count,
                CurrentPage = pageIndex,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }

        // CRUD básico...
        public async Task CreateAsync(Content content) {
            _context.Add(content);
            await _context.SaveChangesAsync();
        }

        public async Task<Content?> GetByIdAsync(int id) {
            return await _context.Contents
                .Include(c => c.Category)
                .Include(c => c.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
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

        // Auxiliares para popular os dropdowns na View
        public async Task<List<Category>> GetCategoriesAsync() => await _context.Categories.ToListAsync();
        public async Task<List<Track>> GetTracksAsync() => await _context.Tracks.ToListAsync();
    }
    public class PaginationResult {
        public List<Content> Items { get; set; } = new();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}