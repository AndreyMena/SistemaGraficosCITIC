using System.ComponentModel.DataAnnotations.Schema;

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
        public Guid? ResearcherId { get; set; } //Null por facilidad para probar insertar project, enrealidad nunca se va a agregar null researcher
        public string? Code { get; set; }
        public virtual List<Researcher> Collaborators { get; set; }
        public List<Publication> Publications { get; set; }
        public List<Exposition> Expositions { get; set; }
        public List<Product> Products { get; set; }

        public Project()
        {
            Id = Guid.NewGuid();
            Name = "";
            Type = "";
            EndDate = DateTime.MinValue;
            Code = "";
            Collaborators = new List<Researcher>();
            Publications = new List<Publication>();
            Expositions = new List<Exposition>();
            Products = new List<Product>();
        }

        public Project(string name, string type, Guid? researcherId, DateTime startDate, DateTime? endDate, bool isActive, string? code)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
            ResearcherId = researcherId;
            Code = code;
            Collaborators = new List<Researcher>();
            Publications = new List<Publication>();
            Expositions = new List<Exposition>();
            Products = new List<Product>();
        }
    }
}