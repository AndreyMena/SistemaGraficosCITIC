using SistemaGraficosCITIC.Data;

namespace SistemaGraficosCITIC.Models.Domain
{
    public class Publication
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        public List<Author> Authors { get; set; }

        public Project? Project { get; set; }

        public Publication()
        {
            Id = Guid.NewGuid();
            Title = "";
            Year = 2023;
            Reference = "";
            Type = "";
            Authors = new List<Author>();
        }
        public Publication(string title, int date, string reference, string type, string[] authors)
        {
            Id = Guid.NewGuid();
            Title = title;
            Year = date;
            Reference = reference;
            Type = type;
            var Aut = new List<Author>();
            DBControllerGetData db = new();
            for (int i = 0; i < authors.Length; i++)
            {
                bool isNewAuthor = false;
                isNewAuthor = db.myValueExist("Author", "AuthorName", authors[i]);
                if (isNewAuthor)
                {
                    var author = new Author(authors[i]);
                    Aut.Add(author);
                }
                else
                {
                    var author = db.GetAuthorByName(authors[i]);
                    Aut.Append(author);
                }
            }
            Authors = Aut;
        }
    }
}