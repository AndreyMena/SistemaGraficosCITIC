namespace SistemaGraficosCITIC.Models.Domain
{
    public class Publication
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
    }
}