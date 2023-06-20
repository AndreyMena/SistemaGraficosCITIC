namespace Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Researcher Researcher { get; set; }
        public Question Question { get; set; }
    }
}