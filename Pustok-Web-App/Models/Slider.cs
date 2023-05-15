using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok_Web_App.Models;

public class Slider
{
    public int Id { get; set; }
    public string Title1 { get; set; } = null!;
    public string Title2 { get; set; }
    public string Description { get; set; }=null!;
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
