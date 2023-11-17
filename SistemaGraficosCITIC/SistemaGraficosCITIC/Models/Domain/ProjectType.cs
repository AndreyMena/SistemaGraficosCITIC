namespace SistemaGraficosCITIC.Models.Domain
{
    public class ProjectType
    {
        public int ProjectTypeId { get; set; }
        public string? ProjectTypeName { get; set; }
        public ProjectType() { }
        public ProjectType(string? projectTypeName)
        {
            ProjectTypeId = 0;
            ProjectTypeName = projectTypeName;
        }
    }
}