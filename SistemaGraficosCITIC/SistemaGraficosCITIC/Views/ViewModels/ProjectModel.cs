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

        public Guid storedId { get; set; }

        [Required]
        public string? PublicationTitle { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string? PublicationReference { get; set; }
        [Required]
        public string? PublicationType { get; set; }

        public List<Publication> ProjectPublications { get; set; }
        public List<Exposition> ProjectExpositions { get; set; }
        public List<Product> ProjectProducts { get; set; }

        
        // Adds entries to the Publication list, doesn't register them in DB yet
        public void addPublication()
        {
            ProjectPublications.Add(new Publication(Guid.Parse(""), PublicationTitle, PublicationDate, PublicationReference, PublicationType) );

            Console.WriteLine("\n\nAdded this Publication to the list:\n\n{PublicationTitle} , {PublicationDate} , {PublicationReference} , {PublicationType}  \n\n");
        }

    }
}
