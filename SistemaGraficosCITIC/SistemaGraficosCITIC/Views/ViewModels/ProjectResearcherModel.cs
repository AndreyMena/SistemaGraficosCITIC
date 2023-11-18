namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ProjectResearcherModel
    {
        public virtual string Name { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string? ProjectId { get; set; }

        public ProjectResearcherModel()
        {
            Name = "";
            LastName = "";
            StartDate = DateTime.MinValue;
        }
        public ProjectResearcherModel(string name, string lastName, DateTime startDate, DateTime endDate)
        {
            Name = name;
            LastName = lastName;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}