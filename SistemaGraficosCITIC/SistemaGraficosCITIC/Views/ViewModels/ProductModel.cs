using System.ComponentModel.DataAnnotations;

namespace SistemaGraficosCITIC.Views.ViewModels
{
    public class ProductModel
    {
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? ProductDescription { get; set; }
        [Required]
        public string? ProductState { get; set; }
        [Required]
        public string? ProductMarketable { get; set; }
        [Required]
        public string? ProductLicense { get; set; }

        public string? ProjectId { get; set; }
    }
}