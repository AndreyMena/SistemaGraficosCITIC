namespace SistemaGraficosCITIC.Models.Domain
{
    public class ProjectResearcher
    {
        public virtual Guid ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public virtual Guid ResearcherId { get; set; }
        public virtual Researcher? Researcher { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
    }
}
