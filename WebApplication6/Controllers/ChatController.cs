using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Core.Models;

namespace WebApplication6.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public ChatController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public  async Task<IActionResult> Index()
        {
            var users= await userManager.Users.ToListAsync();
            return View(users);
        }
    }
}
