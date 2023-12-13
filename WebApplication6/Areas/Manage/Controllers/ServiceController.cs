using Microsoft.AspNetCore.Mvc;
using WebApplication6.DAL;
using WebApplication6.Models;

namespace WebApplication6.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Service> services = _context.Services.ToList();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Services.Add(service);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Update(int id)
        {
            Service wantedService = _context.Services.FirstOrDefault(s => s.Id == id);

            if (wantedService == null) return NotFound();

            return View(wantedService);
        }

        [HttpPost]
        public IActionResult Update(Service service)
        {
            if (!ModelState.IsValid) return View();

            Service existService = _context.Services.FirstOrDefault(x => x.Id == service.Id);

            if (existService == null) return NotFound();

            existService.Title = service.Title;
            existService.Description = service.Description;
            existService.Icon = service.Icon;
           

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Service wantedService = _context.Services.FirstOrDefault(s => s.Id == id);

            if (wantedService == null) return NotFound();

            return View(wantedService);
        }

        [HttpPost]
        public IActionResult Delete(Service service)
        {
            var existService = _context.Services.FirstOrDefault(x => x.Id == service.Id);

            if (existService == null)
            {
                return NotFound();
            }

            _context.Services.Remove(existService);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
