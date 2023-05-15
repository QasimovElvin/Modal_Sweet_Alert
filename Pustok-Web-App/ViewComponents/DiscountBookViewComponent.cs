using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok_Web_App.DAL;
using Pustok_Web_App.Migrations;
using Pustok_Web_App.ViewModel;

namespace Pustok_Web_App.ViewComponents;


public class DiscountBookViewComponent : ViewComponent
{
    private readonly AppDbContext _context;
    public DiscountBookViewComponent(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var books = await _context.Books
            .Include(x => x.BookImages)
            .Include(x => x.Author)
            .Include(x => x.Genre).ToListAsync();

        return View(books);
    }

}

