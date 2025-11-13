using Microsoft.EntityFrameworkCore;
using SyncMe.Data;
using SyncMe.Models;

namespace SyncMe.Services {
    public class ContentService {
        private readonly AppDbContext _context;

        public ContentService(AppDbContext context) {
            _context = context;
        }

        // Método para buscar com filtros e paginação
        public async Task<PaginationResult> GetAllAsync(string searchString, int pageIndex, int pageSize) {
            var query = _context.Contents.AsQueryable();

            // Filtro
            if (!string.IsNullOrEmpty(searchString)) {
                query = query.Where(c => c.Title.Contains(searchString) || c.Category.Contains(searchString));
            }

            // Ordenação
            query = query.OrderByDescending(c => c.PublishDate);

            // Contagem e Paginação
            int count = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginationResult {
                Items = items,
                TotalItems = count,
                CurrentPage = pageIndex,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }

        // Métodos CRUD simples
        public async Task CreateAsync(Content content) {
            _context.Add(content);
            await _context.SaveChangesAsync();
        }

        public async Task<Content?> GetByIdAsync(int id) => await _context.Contents.FindAsync(id);

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
    }

    // Classe auxiliar para transportar os dados da paginação
    public class PaginationResult {
        public List<Content> Items { get; set; } = new();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}