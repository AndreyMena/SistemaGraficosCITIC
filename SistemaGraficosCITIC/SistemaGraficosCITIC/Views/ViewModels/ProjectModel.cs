using SistemaGraficosCITIC.Models.Domain;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ProjectModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public bool isActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ResearcherId { get; set; }
        public IEnumerable<Researcher>? Collaborators { get; set; }
        public string? Code { get; set; }
    }
}
