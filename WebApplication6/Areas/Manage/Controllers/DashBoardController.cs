using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Core.Models;

namespace WebApplication6.Areas.Manage.Controllers
{
    [Area("Manage")]
   // [Authorize(Roles ="Admin,SuperAdmin")]
    public class DashBoardController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DashBoardController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser appUser = new AppUser()
        //    {
        //        FullName = "eli eliyev",
        //        Birthday = "yanvar 10",
        //        UserName = "Elieliyev01"
        //    };

        //  var result= await userManager.CreateAsync(appUser, "Eli2004@");

        //    return Ok(result);
        //}
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("Editor");
        //    IdentityRole role4 = new IdentityRole("Member");


        //    await roleManager.CreateAsync(role1);
        //    await roleManager.CreateAsync(role2);
        //    await roleManager.CreateAsync(role3);
        //    await roleManager.CreateAsync(role4);

        //    return Ok();

        //}

        //public async Task<IActionResult> AddRoleAdmin()
        //{
        //   var appUser =await userManager.FindByNameAsync("Elieliyev01");
        //    await userManager.AddToRoleAsync(appUser, "SuperAdmin");

        //    return Ok();

        //}



    }
}
