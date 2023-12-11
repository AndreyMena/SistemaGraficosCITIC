namespace SistemaGraficosCITIC.Models.Domain
{
  public class Exposition
  {
    public string Title { get; set; }
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string Context { get; set; }
    public string Participants { get; set; }
    public string Speaker { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public Exposition()
    {
      Title = "";
      Id = Guid.NewGuid();
      Date = DateTime.MinValue;
      Location = "";
      Context = "";
      Participants = "";
      Speaker = "";
    }
    public Exposition(string title, DateTime date, string location, string context,
                        string participants, string speaker)
    {
      Title = title;
      Id = Guid.NewGuid();
      Date = date;
      Location = location;
      Context = context;
      Participants = participants;
      Speaker = speaker;
    }

  }
}