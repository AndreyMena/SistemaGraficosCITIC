using SistemaGraficosCITIC.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ProjectTypeModel
    {
        [Required]
        public string? ProjectTypeName { get; set; }

    }
}
