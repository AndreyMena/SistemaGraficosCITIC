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
        public Researcher? Researcher { get; set; } //Null por facilidad para probar insertar project, enrealidad nunca se va a agregar null researcher
        public string? Collaborators { get; set; }
        public string? Code { get; set; }
        public List<Publication> Publications { get; set; } //Se cambiaron de Collection a List
        public List<Exposition> Expositions { get; set; }
        public List<Product> Products { get; set; }

        public Project()
        {
            Id = Guid.NewGuid();
            Name = "";
            Type = "";
            EndDate = DateTime.MinValue;
            Collaborators = "";
            Code = "";
            Publications = new List<Publication>();
            Expositions = new List<Exposition>();
            Products = new List<Product>();
        }

        public Project(string name, string type, Researcher? researcher, DateTime startDate, DateTime? endDate, bool isActive, string? collaborators, string? code)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
            Researcher = researcher; //Por ahora
            Collaborators = collaborators;
            Code = code;
            Publications = new List<Publication>();
            Expositions = new List<Exposition>();
            Products = new List<Product>();
        }
    }
}