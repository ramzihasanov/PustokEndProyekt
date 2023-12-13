using Microsoft.AspNetCore.Identity;
using Pustok.Core.Models;
using WebApplication6.DAL;

namespace WebApplication6.ViewServices
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LayoutService(AppDbContext context,UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Setting>> GetBook()
        {
            var settings = _context.Settings.ToList();

            return settings;
        }

        public async Task<AppUser> GetAppUser()
        {
            AppUser user = null;
          string username=  httpContextAccessor.HttpContext.User.Identity.Name;
            if (username != null)
            {
                user = await userManager.FindByNameAsync(username);
            }
            return user;
        }
    }
}
