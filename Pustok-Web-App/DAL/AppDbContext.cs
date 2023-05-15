using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pustok_Web_App.Models;

namespace Pustok_Web_App.DAL;

public class AppDbContext:IdentityDbContext<ApplicationUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
	{

	}

    public DbSet<Slider> Sliders { get; set; }
	public DbSet<Author> AuthorS { get; set; }
	public DbSet<Book> Books { get; set; }
	public DbSet<BookImage> BookImages { get; set; }
	public DbSet<Genre> Genres { get; set; }

}
