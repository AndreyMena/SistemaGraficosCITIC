namespace SistemaGraficosCITIC.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Researcher Researcher { get; set; }
        public ICollection<Publication> Publications { get; set; }
        public ICollection<Exposition> Expositions { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}