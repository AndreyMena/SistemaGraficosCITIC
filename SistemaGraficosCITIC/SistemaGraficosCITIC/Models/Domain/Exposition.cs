namespace SistemaGraficosCITIC.Models.Domain
{
    public class Exposition
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Context { get; set; }

        public Project? Project { get; set; }

        public Exposition()
        {
            Id = Guid.NewGuid();
            Date = DateTime.MinValue;
            Location = "";
            Context = "";
        }
        public Exposition(DateTime date, string location, string context)
        {
            Id = Guid.NewGuid();
            Date = date;
            Location = location;
            Context = context;
        }

    }
}