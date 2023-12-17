using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.DAL;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext appDbContext;

        public ShopController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index(int? genreId)
        {
            var query = appDbContext.Books.Include(x => x.BookImages).Include(x => x.Author).AsQueryable();

            if (genreId != null)  query = query.Where(x => x.GenreId == genreId);
             ShopViewModel model = new ShopViewModel()
            {
                Books = await query.ToListAsync(),
                Genres = await appDbContext.Genres.Include(x => x.Books).ToListAsync(),
            };

            return View(model);
        }
    }
}