using System.Xml.Linq;

namespace SistemaGraficosCITIC.Models.Domain
{
    public class Publication
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        // Public List<string> Authors {get; set;}

        public Project? Project { get; set; }

        public Publication()
        {
            Id = Guid.NewGuid();
            Title = "";
            Date = DateTime.MinValue;
            Reference = "";
            Type = "";
            //Authors = "";
        }
        public Publication(string title, DateTime date, string reference, string type /*, List<String> authors*/)
        {
            Id = Guid.NewGuid();
            Title = title;
            Date = date;
            Reference = reference;
            Type = type;
            // Authors = authors;
        }

    }
}