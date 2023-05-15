using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok_Web_App.Models;
using Pustok_Web_App.ViewModel.Account;

namespace Pustok_Web_App.Controllers
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

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrVM registrVM)
        {
            if (!ModelState.IsValid)return View();
            ApplicationUser NewUser = new ApplicationUser()
            {
                Name=registrVM.Name,
                Surname=registrVM.Surname,
                UserName=registrVM.UserName,
                Email=registrVM.Email,
            };
            IdentityResult result=await _userManager.CreateAsync(NewUser,registrVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(NewUser, "member");
            await _signInManager.SignInAsync(NewUser,false);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();
            if (loginVM.EmailOrUsername.Contains("@"))
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(loginVM.EmailOrUsername);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Please Wait");
                        return View();
                    }
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Invalid Credantials");
                        return View();
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginVM.EmailOrUsername);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Please Wait");
                        return View();
                    }
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Invalid Credentials");
                        return View();
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            return NoContent();

        }
    } 
}
