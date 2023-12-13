using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.DAL;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            HomeViewModels homeViewModels = new HomeViewModels()
            {
                Sliders = _context.Slider.ToList(),
                Services = _context.Services.ToList(),
                NewBooks = _context.Books.Include(b => b.BookImages).Include(a => a.Author).Where(b => b.isNew).ToList(),
                FeaturedBooks = _context.Books.Include(b => b.BookImages).Include(a => a.Author).Where(b => b.isFeatured).ToList(),
                BestsellerBooks = _context.Books.Include(b => b.BookImages).Include(a => a.Author).Where(b => b.isBestseller).ToList()

        };
            return View(homeViewModels);
        }

     
    }
}