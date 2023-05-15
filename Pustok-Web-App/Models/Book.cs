using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok_Web_App.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public int DisCountPrice { get; set; }
    public decimal? Rate { get; set; }
    public string? Discraption { get; set; }
    public int PageCount { get; set; }
    public ICollection<BookImage>? BookImages { get; set; }

    [NotMapped]
    public IFormFile? MainFile { get; set; }

    [NotMapped]

    public List<IFormFile>? Files { get; set; }

}
