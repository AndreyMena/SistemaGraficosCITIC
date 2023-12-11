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
    public bool IsActive { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ResearcherId { get; set; } // Main researcher id
    public string[]? Collaborators { get; set; } // It pass this information to middle table
    public string? Code { get; set; }
  }
}
