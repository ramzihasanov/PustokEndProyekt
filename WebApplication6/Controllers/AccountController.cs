using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Core.Models;
using System.Data;
using WebApplication6.DAL;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly AppDbContext context;

        public AccountController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,SignInManager<AppUser> signInManager,AppDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.context = context;
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
        public async Task<IActionResult> Login(MemberLoginViewModel memberLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = null;

            user = await userManager.FindByNameAsync(memberLoginVM.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(user, memberLoginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            return RedirectToAction("index", "home");

        }

        public  IActionResult Register()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterViewModel)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = null;
            appUser = await userManager.FindByNameAsync(memberRegisterViewModel.Username);
            if(appUser != null)
            {
                ModelState.AddModelError("Username", "username have a account");
                return View();
            }
            appUser = await userManager.FindByEmailAsync(memberRegisterViewModel.Email);
            if (appUser != null)
            {
                ModelState.AddModelError("Username", "username have a account");
                return View();
            }
            AppUser appUser1 = new AppUser()
            {
                FullName = memberRegisterViewModel.Fullname,
                UserName = memberRegisterViewModel.Username,
                Email = memberRegisterViewModel.Email,
                Birthday = memberRegisterViewModel.Birthday
            };

           var result= await userManager.CreateAsync(appUser1, memberRegisterViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description );
                    return View();
                }
            }
          await userManager.AddToRoleAsync(appUser1, "Member");
            await signInManager.SignInAsync(appUser1, isPersistent: false);
            return RedirectToAction("Index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [Authorize(Roles = "Member,Admin, SuperAdmin")]
        public async Task<IActionResult> Profile()
        {
            AppUser appUser = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            List<Order> orders = await context.Orders
                                        .Where(x => x.AppUserId == appUser.Id)
                                        .ToListAsync();

            return View(orders);
        }
    }
}


    

