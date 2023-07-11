using SistemaGraficosCITIC.Models.Domain;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class IndexProjectsViewModel
    {
        public IEnumerable<Project>? projects { get; set; }
        public IEnumerable<Publication>? publications { get; set; }
    }
}
