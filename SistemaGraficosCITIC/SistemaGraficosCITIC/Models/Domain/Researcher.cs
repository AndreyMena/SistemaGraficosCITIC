using System.Xml.Linq;

namespace SistemaGraficosCITIC.Models.Domain
{
    public class Researcher
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Type { get; set; }


        public Researcher()
        {
            Id = Guid.NewGuid();
            Name = "";
            LastName = "";
            Type = "";
        }
        public Researcher(string name, string lastName, string type)
        {
            Id = Guid.NewGuid();
            Name = name;
            LastName = lastName;
            Type = type;
        }
    }
}