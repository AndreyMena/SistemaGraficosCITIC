namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ResearcherModel
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public List<string> Types { get; set; }


        public ResearcherModel()
        {
            Name = "";
            LastName = "";
            Types = new List<string>();
        }
        public ResearcherModel(string name, string lastName, List<string> types)
        {
            Name = name;
            LastName = lastName;
            Types = types;
        }
    }
}