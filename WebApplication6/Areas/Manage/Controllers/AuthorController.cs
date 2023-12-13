using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.DAL;
using WebApplication6.Models;

namespace WebApplication6.Areas.Manage.Controllers
{

    [Area("Manage")]
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var author = _context.Authors.ToList();
            return View(author);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid) return View(author);

            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var wanted = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Author author)
        {
            Author existAuthor = _context.Authors.FirstOrDefault(x => x.Id == author.Id);
            if (existAuthor == null) return NotFound();
            if (!ModelState.IsValid) return View(existAuthor);

            existAuthor.Name = author.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Author author)
        {
            var wanted = _context.Authors.FirstOrDefault(x => x.Id == author.Id);
            if (wanted == null) return NotFound();
            _context.Authors.Remove(wanted);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
