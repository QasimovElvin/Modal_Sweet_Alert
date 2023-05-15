using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok_Web_App.DAL;
using Pustok_Web_App.Migrations;
using Pustok_Web_App.ViewModel;

namespace Pustok_Web_App.ViewComponents;


public class BasketViewComponent : ViewComponent
{
    private readonly AppDbContext _context;
    public BasketViewComponent(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<BasketVM>? basket = GetBasket();
        List<BasketItemVM> basketItems = new List<BasketItemVM>();
        foreach (var basketVM in basket)
        {
            var book = await  _context.Books.Include(x => x.BookImages).FirstOrDefaultAsync(x => x.Id == basketVM.Id);
            basketItems.Add(new BasketItemVM
            {
                Id = book.Id,
                Count = basketVM.Count,
                Name = book.Name,
                Price=book.Price,
                Image = book.BookImages.FirstOrDefault(x=>x.IsMain).Image
                
            });
        }
        return View(basketItems);
    }
    private List<BasketVM> GetBasket()
    {
        List<BasketVM> basket = new List<BasketVM>();
        if (Request.Cookies["basket"] != null)
        {
            basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);

        }
       
        return basket;

    }
}
