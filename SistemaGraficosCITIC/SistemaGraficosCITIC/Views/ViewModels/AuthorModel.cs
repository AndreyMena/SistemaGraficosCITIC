using SistemaGraficosCITIC.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class AuthorModel
    {
        [Required]
        public string? AuthorName { get; set; }
        public string? PublicationId { get; set; }
        // public List<PublicationModel>? PublicationsModel { get; set; }
    }
}
