using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncMe.Models {
    [Table("TB_CONTENT")] // Nome da tabela no Oracle
    public class Content {
        [Key]
        [Column("ID_CONTENT")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        [Column("NM_TITLE")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Summary is required")]
        [Column("DS_SUMMARY")]
        public string Summary { get; set; }

        [Column("DS_MEDIA_URL")]
        public string? MediaUrl { get; set; }

        [Required]
        [Column("DT_PUBLISH")]
        public DateTime PublishDate { get; set; }

        // Enums are great for fixed options like Difficulty
        [Column("TP_DIFFICULTY")]
        public DifficultyLevel Difficulty { get; set; }

        [Column("NM_CATEGORY")]
        public string Category { get; set; } // Ex: "Mental Health", "Soft Skills"

        public Content() {
            PublishDate = DateTime.Now;
        }
    }

    public enum DifficultyLevel {
        Beginner = 0,
        Intermediate = 1,
        Advanced = 2
    }
}