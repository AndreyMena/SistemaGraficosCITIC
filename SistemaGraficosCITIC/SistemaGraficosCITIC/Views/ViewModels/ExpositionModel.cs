using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ExpositionModel
    {
        public string? ExpositionTitle { get; set; }

        [Required]
        public DateTime ExpositionDate { get; set; }
        [Required]
        public string? ExpositionLocation { get; set; }
        [Required]
        public string? ExpositionContext { get; set; }

        public string? ProjectId { get; set; }

        public string? ExpositionParticipants { get; set; }

        public string? ExpositionSpeaker { get; set; }
    }
}
