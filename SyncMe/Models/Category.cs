using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncMe.Models {
    [Table("TB_CATEGORY")]
    public class Category {
        [Key]
        [Column("ID_CATEGORY")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("NM_NAME")]
        public string Name { get; set; } // Ex: "Saúde Mental", "Produtividade"

        // Relacionamento: Uma categoria tem vários conteúdos
        public ICollection<Content> Contents { get; set; } = new List<Content>();
    }
}