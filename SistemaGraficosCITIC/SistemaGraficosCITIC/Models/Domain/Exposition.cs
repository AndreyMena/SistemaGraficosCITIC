namespace SistemaGraficosCITIC.Models.Domain
{
    public class Exposition
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Context { get; set; }
    }
}