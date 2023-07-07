using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Models.Domain;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ProjectModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public bool isActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CounterPublications { get; set; } = 0;
        public int CounterExpositions { get; set; } = 0;
        public int CounterProducts { get; set; } = 0;

    }
}
