namespace Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Boolean Active { get; set; }
    }
}