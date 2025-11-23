using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncMe.Models {
    [Table("TB_GS_TRACK")]
    public class Track {
        [Key]
        [Column("ID_TRACK")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NM_TITLE")]
        public string Title { get; set; } // Ex: "Semana Sem Stress"

        [Column("DS_DESCRIPTION")]
        public string? Description { get; set; }

        // Relacionamento: Uma trilha tem vários conteúdos
        public ICollection<Content> Contents { get; set; } = new List<Content>();
    }
}