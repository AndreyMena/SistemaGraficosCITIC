namespace SistemaGraficosCITIC.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string Marketable { get; set; }
        public string License { get; set; }

        public Project? Project { get; set; }

        public Product()
        {
            Id = Guid.NewGuid();
            Name = "";
            Description = "";
            State = "";
            Marketable = "";
            License = "";
        }
        public Product(string name, string description, string state, string marketable, string license)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            State = state;
            Marketable = marketable;
            License = license;
        }
    }
}