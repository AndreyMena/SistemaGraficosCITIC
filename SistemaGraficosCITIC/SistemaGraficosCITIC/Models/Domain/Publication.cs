using System.Xml.Linq;

namespace SistemaGraficosCITIC.Models.Domain
{
    public class Publication
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        public string Author {get; set;}

        public Project? Project { get; set; }

        public Publication()
        {
            Id = Guid.NewGuid();
            Title = "";
            PublicationYear = 2023;
            Reference = "";
            Type = "";
            Author = "";
        }
        public Publication(string title, int date, string reference, string type, string authors)
        {
            Id = Guid.NewGuid();
            Title = title;
            PublicationYear = date;
            Reference = reference;
            Type = type;
            Author = authors;
        }

    }
}