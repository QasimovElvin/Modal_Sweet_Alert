using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_Web_App.DAL;
using Pustok_Web_App.Models;
using Pustok_Web_App.Utilities.Extension;

namespace Pustok_Web_App.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BookController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _environment;

        public BookController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.Include(x=>x.Genre).Include(x=>x.Author).Include(x=>x.BookImages).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _context.AuthorS.ToListAsync();
            ViewBag.Genres = await _context.Genres.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            ViewBag.Authors = await _context.AuthorS.ToListAsync();
            ViewBag.Genres = await _context.Genres.ToListAsync();
            if (!ModelState.IsValid) return View();
            if (!book.MainFile.CheckType("image/"))
            {
                ModelState.AddModelError("IsMain", "Invalid Type");
                return View();
            }
            if (book.MainFile.CheckSizeFile(2000))
            {
                ModelState.AddModelError("IsMain", "File Size Invalid");
                return View();
            }
            book.BookImages = new List<BookImage>();

            book.BookImages.Add(new BookImage
            {
                IsMain = true,
                Image = await book.MainFile.SaveFileAsync(_environment.WebRootPath, "assets/image/products/"),
                Book = book
            });
            if(book.Files != null)
            {
                foreach (var item in book.Files)
                {
                    if (item.CheckType("image/"))
                    {
                        ModelState.AddModelError("IsMain", "Invalid Type");
                        return View();
                    }
                    if (item.CheckSizeFile(2000))
                    {
                        ModelState.AddModelError("IsMain", "File Size Invalid");
                        return View();
                    }
                    book.BookImages.Add(new BookImage
                    {
                        IsMain = false,
                        Image = await item.SaveFileAsync(_environment.WebRootPath, "assets/image/products/"),
                        Book = book
                    });
                }
            }
        
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
