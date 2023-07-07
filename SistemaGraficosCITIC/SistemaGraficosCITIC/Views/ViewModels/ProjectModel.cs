using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGraficosCITIC.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ProjectModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Type { get; set; }
        public bool isActive { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<string>? listaName { get; set; }    

        public int CounterPublications { get; set; } = 0;
        public int CounterExpositions { get; set; } = 0;
        public int CounterProducts { get; set; } = 0;

    }
}
