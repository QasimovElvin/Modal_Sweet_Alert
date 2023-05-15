using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Pustok_Web_App.DAL;
using Pustok_Web_App.Models;
using Pustok_Web_App.Utilities.Extension;

namespace Pustok_Web_App.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _enviroment;
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context, IWebHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)return View(slider);
            if (!slider.ImageFile.CheckType("image/"))
            {
                ModelState.AddModelError("ImageFile", "Image Type Incorrect");
                return View();
            }
            if (slider.ImageFile.CheckSizeFile(200))
            {
                ModelState.AddModelError("ImageFile", "Image Size Invalid");
                return View();
            }
            slider.Image = await slider.ImageFile.SaveFileAsync(_enviroment.WebRootPath, "assets/image");
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Slider slider)
        {
            Slider? exists = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == slider.Id);

            
            if (!slider.ImageFile.CheckType("image/"))
            {
                ModelState.AddModelError("ImageFile", "Image Type Incorrect");
                return View();
            }
            if (slider.ImageFile.CheckSizeFile(200))
            {
                ModelState.AddModelError("ImageFile", "Image Size Invalid");
                return View();
            }
            exists.Image = await slider.ImageFile.SaveFileAsync(_enviroment.WebRootPath, "slider");
            exists.Title1 = slider.Title1;
            exists.Title2 = slider.Title2;
            exists.Description = slider.Description;
            exists.Image = slider.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider? exists=await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null) return Json(new {status=404});
            _context.Sliders.Remove(exists);
            await _context.SaveChangesAsync();
            return Json(new { status =200});
        }
    }
}
