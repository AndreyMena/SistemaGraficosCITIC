using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ResearcherTypeModel
    {
        [Required]
        public string? ResearcherTypeName { get; set; }

    }
}
