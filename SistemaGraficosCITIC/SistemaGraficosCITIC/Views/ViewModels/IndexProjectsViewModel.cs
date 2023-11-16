using SistemaGraficosCITIC.Models.Domain;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class IndexProjectsViewModel
    {
        public IEnumerable<Project>? projects { get; set; }
        public IEnumerable<Publication>? publications { get; set; }
        public IEnumerable<Exposition>? expositions { get; set; }
        public IEnumerable<Product>? products { get; set; }
        public IEnumerable<Author>? authors { get; set; }
        public IEnumerable<PublicationType>? publicationTypes { get; set; }
    }
}
