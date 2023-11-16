namespace SistemaGraficosCITIC.Models.Domain
{
    public class ResearcherTypeResearcher
    {
        public virtual Guid ResearcherId { get; set; }

        public virtual Researcher Researcher { get; set; }

        public virtual int ResearcherTypeId { get; set; }

        public virtual ResearcherTypes ResearcherTypes { get; set; }
    }
}