using System.Data;

namespace SistemaGraficosCITIC.Models.Domain
{
    public class AuthorPublication
    {
        public virtual Guid PublicationId { get; set; }

        public virtual Publication Publication { get; set; }

        public virtual Guid AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}