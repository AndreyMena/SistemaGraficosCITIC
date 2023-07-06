namespace SistemaGraficosCITIC.Models.Domain
{
    public class Researcher
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Type { get; set; }
        public ICollection<Project>? Projects { get; set; }
    }
}