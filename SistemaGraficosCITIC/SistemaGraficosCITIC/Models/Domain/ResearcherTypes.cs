using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Models.Domain
{
    public class ResearcherTypes
    {
        [Key]
        public int ResearcherTypeId { get; set; }
        public string? ResearcherTypeName { get; set; }
        public virtual List<Researcher> Researchers { get; set; }

        public ResearcherTypes() 
        {
            ResearcherTypeId = 0;
            ResearcherTypeName = "";
            Researchers = new List<Researcher>();
        }
        public ResearcherTypes(string? researcherTypeName)
        {
            ResearcherTypeId = 0;
            ResearcherTypeName = researcherTypeName;
            Researchers = new List<Researcher>();
        }
    }
}
