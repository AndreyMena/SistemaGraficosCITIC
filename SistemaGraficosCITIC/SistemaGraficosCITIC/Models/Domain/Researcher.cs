namespace SistemaGraficosCITIC.Models.Domain
{
  public class Researcher
  {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Type { get; set; }
    public ICollection<ResearcherTypes> TypeList { get; set; }
    public virtual ICollection<Project> Projects { get; set; }

    public Researcher()
    {
      Id = Guid.NewGuid();
      Name = "";
      LastName = "";
      Type = "";
      TypeList = new List<ResearcherTypes>();
      Projects = new List<Project>();
    }
    public Researcher(string name, string lastName, string type)
    {
      Id = Guid.NewGuid();
      Name = name;
      LastName = lastName;
      Type = type;
      TypeList = new List<ResearcherTypes>();
      Projects = new List<Project>();
    }
  }
}