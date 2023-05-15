using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok_Web_App.Models;

namespace Pustok_Web_App.Areas.AdminPanel.Controllers
{
	public class AccountController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
  //      public async Task<IActionResult> Index()
		//{
        //    ApplicationUser admin = new ApplicationUser()
        //    {
        //        UserName = "Admin",
        //        Name = "Admin",
        //        Surname = "Mudur",
        //        Email="admin@gmail.com"
        //    };
        //    var result = await _userManager.CreateAsync(admin, "Admin123!");
        //    if (!result.Succeeded)
        //    {
        //        foreach (var item in result.Errors)
        //        {
        //            ModelState.AddModelError("", item.Description);
        //        }
        //        return View();
        //    }
        //    //await _roleManager.CreateAsync(new IdentityRole("admin"));
        //    //await _roleManager.CreateAsync(new IdentityRole("member"));
        //    var user = await _userManager.FindByNameAsync("admin");

        //    await _userManager.AddToRoleAsync(user, "admin");
        //    await _signInManager.SignInAsync(user, false);
        //    return Json("Done");
        //}
        
	}
}
