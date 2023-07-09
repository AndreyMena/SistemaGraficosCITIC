namespace SistemaGraficosCITIC.Models.Domain
{
    public class Publication
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }

        public Publication(Guid id, string title, DateTime date, string reference, string type)
        {
            Id = id;
            Title = title;
            Date = date;
            Reference = reference;
            Type = type;
        }

    }
}