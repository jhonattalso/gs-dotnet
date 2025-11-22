using Microsoft.EntityFrameworkCore;
using SyncMe.Models;

namespace SyncMe.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<Content> Contents { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Track> Tracks { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // 1. Seed de Categorias
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Saúde Mental" },
                new Category { Id = 2, Name = "Produtividade" },
                new Category { Id = 3, Name = "Soft Skills" }
            );

            // 2. Seed de Trilhas
            modelBuilder.Entity<Track>().HasData(
                new Track { Id = 1, Title = "Semana Sem Stress", Description = "Guia prático para reduzir a ansiedade no trabalho." },
                new Track { Id = 2, Title = "Liderança 4.0", Description = "Como gerir equipes remotas." }
            );

            // 3. Seed de Conteúdos (Atualizado com CategoryId)
            modelBuilder.Entity<Content>().HasData(
                new Content {
                    Id = 1,
                    Title = "Técnica Pomodoro",
                    Summary = "Aprenda a gerenciar seu tempo com pausas estratégicas.",
                    Difficulty = DifficultyLevel.Iniciante,
                    PublishDate = DateTime.Now,
                    MediaUrl = "https://www.youtube.com/watch?v=123",
                    CategoryId = 2, // Produtividade
                    TrackId = 1     // Semana Sem Stress
                },
                new Content {
                    Id = 2,
                    Title = "Mindfulness no Trabalho",
                    Summary = "Exercícios rápidos de respiração.",
                    Difficulty = DifficultyLevel.Iniciante,
                    PublishDate = DateTime.Now,
                    MediaUrl = "https://www.youtube.com/watch?v=456",
                    CategoryId = 1, // Saúde Mental
                    TrackId = 1     // Semana Sem Stress
                }
            );
        }
    }
}