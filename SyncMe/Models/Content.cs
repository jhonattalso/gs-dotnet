using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncMe.Models {
    [Table("TB_CONTENT")]
    public class Content {
        [Key]
        [Column("ID_CONTENT")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100)]
        [Column("NM_TITLE")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O resumo é obrigatório")]
        [StringLength(300, ErrorMessage = "O resumo deve ter no máximo 300 caracteres")]
        [Column("DS_SUMMARY")]
        public string Summary { get; set; }

        // Adicione o limite aqui também
        [StringLength(2000, ErrorMessage = "O conteúdo não pode exceder 2000 caracteres")]
        [Column("DS_ARTICLE_BODY")]
        public string? ArticleBody { get; set; }

        [Column("DS_COVER_IMAGE_URL")]
        public string? CoverImageUrl { get; set; }

        [Column("DS_MEDIA_URL")]
        public string? MediaUrl { get; set; }

        [Required]
        [Column("DT_PUBLISH")]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Column("TP_DIFFICULTY")]
        public DifficultyLevel Difficulty { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria")]
        [Column("ID_CATEGORY")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Column("ID_TRACK")]
        public int? TrackId { get; set; }

        [ForeignKey("TrackId")]
        public Track? Track { get; set; }
    }

    public enum DifficultyLevel {
        Iniciante = 0,
        Intermediario = 1,
        Avancado = 2
    }
}