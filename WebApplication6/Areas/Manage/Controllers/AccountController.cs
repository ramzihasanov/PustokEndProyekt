using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Core.Models;
using WebApplication6.Areas.ViewModels;

namespace WebApplication6.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel)
        {
            if(!ModelState.IsValid) return View(adminLoginViewModel);
            AppUser appUser = null;
           appUser=await userManager.FindByNameAsync(adminLoginViewModel.Username);
            if(appUser == null)
            {
                ModelState.AddModelError("", "invalid username or paswword");
                return View();
            }
         var result=  await signInManager.PasswordSignInAsync(appUser, adminLoginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "invalid username or paswword");
                return View();
            }

            return RedirectToAction("Index","DashBoard");
        }
    }
}
