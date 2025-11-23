using Microsoft.AspNetCore.Mvc.Rendering;
using SyncMe.Models;
using System.ComponentModel.DataAnnotations;

namespace SyncMe.ViewModels {
    public class ContentViewModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [Display(Name = "Título do Conteúdo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O resumo é obrigatório.")]
        [StringLength(300, ErrorMessage = "O resumo deve ter no máximo {1} caracteres.")] // <--- AQUI
        [Display(Name = "Resumo")]
        public string Summary { get; set; }

        [Display(Name = "Corpo do Artigo")]
        [StringLength(2000, ErrorMessage = "O conteúdo completo não pode passar de {1} caracteres.")] // <--- AQUI
        public string? ArticleBody { get; set; }

        [Display(Name = "URL da Imagem de Capa")]
        public string? CoverImageUrl { get; set; }

        [Display(Name = "Link da Mídia (YouTube/Imagem)")]
        public string? MediaUrl { get; set; }

        [Display(Name = "Nível de Dificuldade")]
        public DifficultyLevel Difficulty { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        public SelectList? Categories { get; set; }

        [Display(Name = "Trilha de Aprendizado")]
        public int? TrackId { get; set; }
        public SelectList? Tracks { get; set; }
    }
}