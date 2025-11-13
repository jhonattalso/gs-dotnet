using Microsoft.EntityFrameworkCore;
using SyncMe.Models;

namespace SyncMe.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<Content> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Seed Data (Dados iniciais para a Global Solution)
            modelBuilder.Entity<Content>().HasData(
                new Content {
                    Id = 1,
                    Title = "Pomodoro Technique",
                    Summary = "Learn to manage your time effectively.",
                    Category = "Productivity",
                    Difficulty = DifficultyLevel.Beginner,
                    PublishDate = DateTime.Now,
                    MediaUrl = "https://youtube.com/..."
                },
                new Content {
                    Id = 2,
                    Title = "Workplace Mindfulness",
                    Summary = "Reduce stress with breathing exercises.",
                    Category = "Mental Health",
                    Difficulty = DifficultyLevel.Beginner,
                    PublishDate = DateTime.Now,
                    MediaUrl = "https://youtube.com/..."
                }
            );
        }
    }
}