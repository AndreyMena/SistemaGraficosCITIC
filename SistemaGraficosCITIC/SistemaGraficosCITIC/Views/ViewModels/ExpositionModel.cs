using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ExpositionModel
    {
        [Required]
        public DateTime ExpositionDate { get; set; }
        [Required]
        public string? ExpositionLocation { get; set; }
        [Required]
        public string? ExpositionContext { get; set; }
        
        public string? ProjectId { get; set; }
    }
}
