using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class PublicationModel
    {
        [Required]
        public string? PublicationTitle { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string? PublicationReference { get; set; }
        [Required]
        public string? PublicationType { get; set; }
        public string? ProjectId { get; set; }
    }
}
