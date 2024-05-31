using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Webapp.Components.Pages.Farmer;

public sealed class AddProductRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Category { get; set; } 

    [Required]
    public DateTime ProductionDate { get; set; } = DateTime.Now;
    
    [Required]
    public string ImageData { get; set; }
} 