using SistemaGraficosCITIC.Data;

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
    public Guid ResearcherId { get; set; } //Null por facilidad para probar insertar project, enrealidad nunca se va a agregar null researcher
    public ICollection<Researcher> Collaborators { get; set; }
    public string? Code { get; set; }
    public ICollection<Publication> Publications { get; set; } //Se cambiaron de Collection a List
    public List<Exposition> Expositions { get; set; }
    public List<Product> Products { get; set; }

    public Project()
    {
      Id = Guid.NewGuid();
      Name = "";
      Type = "";
      EndDate = DateTime.MinValue;
      Collaborators = new List<Researcher>();
      Code = "";
      Publications = new List<Publication>();
      Expositions = new List<Exposition>();
      Products = new List<Product>();
    }

    public Project(string name, string type, DateTime startDate, DateTime? endDate, bool isActive, Guid researcherId, string? code, List<Researcher> collaborators)
    {
      Id = Guid.NewGuid();
      Name = name;
      Type = type;
      StartDate = startDate;
      EndDate = endDate;
      IsActive = isActive;
      ResearcherId = researcherId; //Main researcher id
      Collaborators = collaborators;
      Code = code;
      Publications = new List<Publication>();
      Expositions = new List<Exposition>();
      Products = new List<Product>();
    }
  }
}