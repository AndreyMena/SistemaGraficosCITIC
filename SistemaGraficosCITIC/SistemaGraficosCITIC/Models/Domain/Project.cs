using System.Xml.Linq;

namespace SistemaGraficosCITIC.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public Researcher? Researcher { get; set; } //Null por facilidad para probar insertar project, enrealidad nunca se va a agregar null researcher
        public List<Researcher> Researchers { get; set; }
        public List<Publication> Publications { get; set; } //Se cambiaron de Collection a List
        public List<Exposition> Expositions { get; set; }
        public List<Product> Products { get; set; }

        public Project()
        {
            Id = Guid.NewGuid();
            Name = "";
            Code = "";
            Type = "";
            EndDate = DateTime.MinValue;
            Researchers = new List<Researcher>();
            Publications = new List<Publication>();
            Expositions = new List<Exposition>();
            Products = new List<Product>();
        }

        public Project(string name, string type, string code, Researcher? researcher, DateTime startDate, DateTime? endDate, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            Code = code;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
            Researcher = researcher; //Por ahora
            Researchers = new List<Researcher>();
            Publications = new List<Publication>();
            Expositions = new List<Exposition>();
            Products = new List<Product>();
        }
    }
}