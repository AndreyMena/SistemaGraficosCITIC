using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ProjectModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Type { get; set; }
        public bool isActive { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
