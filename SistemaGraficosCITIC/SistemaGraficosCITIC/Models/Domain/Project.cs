namespace SistemaGraficosCITIC.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public Researcher Researcher { get; set; }
        public List<Publication> Publications { get; set; } //Se cambiaron de Collection a List
        public List<Exposition> Expositions { get; set; }
        public List<Product> Products { get; set; }

        public Project(string name, string type, DateTime startDate, DateTime? endDate, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
            Researcher = null!; //Por ahora
            Publications = new List<Publication>();
            Expositions = new List<Exposition>();
            Products = new List<Product>();
        }
    }
}