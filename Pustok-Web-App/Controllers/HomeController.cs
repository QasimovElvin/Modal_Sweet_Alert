using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok_Web_App.DAL;
using Pustok_Web_App.Models;
using Pustok_Web_App.ViewModel;

namespace Pustok_Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

        public async Task<IActionResult> AddBasket(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book? book = _context.Books.FirstOrDefault(x => x.Id == id);
            List<BasketVM> basket = new List<BasketVM>();
            BasketVM basketItem = null;
            if (Request.Cookies["Books"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["Books"]);
                basketItem = basket.FirstOrDefault(x => x.Id == id);
            }
            if (basketItem == null)
            {
                basket.Add(new BasketVM()
                {
                    Id = book.Id,
                    Count = 1
                });
            }
            else
            {
                basketItem.Count++;
            }
            Response.Cookies.Append("Books", JsonConvert.SerializeObject(basket));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetBook(int id)
        {
            Book book = await _context.Books.
                Include(x=>x.Author).
                Include(x=>x.Genre).
                Include(x=>x.BookImages)
                .FirstOrDefaultAsync(x=>x.Id == id);
            if(book == null)return NotFound();
            return PartialView("_BookModalPartial",book);
        }
    }
}
