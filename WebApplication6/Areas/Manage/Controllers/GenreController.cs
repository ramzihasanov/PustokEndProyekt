using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pustok.Business.CustomExceptions.GenreException;
using WebApplication6.DAL;
using WebApplication6.Models;
using WebApplication6.Services.Interfaces;

namespace WebApplication6.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GenreController : Controller
    {
        
        private readonly IGenreService _genreService;

        public GenreController( IGenreService genreService)
        {
           
            _genreService = genreService;
        }
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllAsync();
            return View(genres);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (!ModelState.IsValid) return View(genre);
         

           await _genreService.CreateAsync(genre);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Genre wanted = await _genreService.GetByIdAsync(id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Genre genre)
        {
            //Genre existGenre = _appDb.Genres.FirstOrDefault(x => x.Id == genre.Id);
            //if (existGenre == null) return NotFound();
            if (!ModelState.IsValid) return View();

            await _genreService.UpdateAsync(genre);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

           await _genreService.Delete(id);

            return Ok();
        }
 }   }