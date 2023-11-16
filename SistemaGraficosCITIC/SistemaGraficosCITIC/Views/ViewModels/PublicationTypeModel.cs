using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class PublicationTypeModel
    {
        [Required]
        public string? PublicationTypeName { get; set; }

    }
}
