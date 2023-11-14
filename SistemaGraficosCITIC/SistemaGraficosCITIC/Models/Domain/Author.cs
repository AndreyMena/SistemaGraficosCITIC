using SistemaGraficosCITIC.Views.ViewModels;

namespace SistemaGraficosCITIC.Models.Domain
{
    public class Author
    {
        public Guid AuthorId { get; set; }

        public string AuthorName { get; set; }

        public virtual List<Publication> Publications { get; set; }

        public Author()
        {
            AuthorId = Guid.NewGuid();
            AuthorName = "";
            Publications = new List<Publication>();
        }

        public Author(string name)
        {   
            AuthorId = Guid.NewGuid();
            AuthorName = name;
            Publications = new List<Publication>();
        }
        public Author(Guid authorId, string authorName, List<Publication> publications)
        {
            AuthorId = authorId;
            AuthorName = authorName;
            Publications = publications;
        }
    }
}