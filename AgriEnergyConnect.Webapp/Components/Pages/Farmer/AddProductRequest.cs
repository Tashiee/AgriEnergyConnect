
using System.ComponentModel.DataAnnotations;


namespace AgriEnergyConnect.Webapp.Components.Pages.Farmer
{
    
    public sealed class AddProductRequest
    {
        // Define the Name property with required validation.
        [Required]
        public string Name { get; set; }

        // Define the Category property with required validation.
        [Required]
        public string Category { get; set; }

        // Define the ProductionDate property with required validation.
        // Set a default value of DateTime.Now if not provided.
        [Required]
        public DateTime ProductionDate { get; set; } = DateTime.Now;

        // Define the ImageData property with required validation.
        [Required]
        public string ImageData { get; set; }
    }
}
