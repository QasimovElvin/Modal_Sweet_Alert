using Microsoft.AspNetCore.Mvc;

namespace Pustok_Web_App.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
