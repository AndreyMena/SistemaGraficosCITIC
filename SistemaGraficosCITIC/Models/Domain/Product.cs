namespace SistemaGraficosCITIC.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string Markeatable { get; set; }
        public string License { get; set; }
    }
}