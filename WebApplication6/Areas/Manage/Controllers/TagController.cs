using Microsoft.AspNetCore.Mvc;
using WebApplication6.DAL;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;
using WebApplication6.Services.Interfaces;

namespace WebApplication6.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly ITagRepository _tagRepository;

        public TagController(ITagService tagService,ITagRepository tagRepository)
        {
            _tagService = tagService;
            _tagRepository = tagRepository;
        }
        public IActionResult Index()
        {
            List<Tag> tags = _tagRepository.Table.ToList();

            return View(tags);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid) return View(tag);

          

           await _tagService.CreateAsync(tag);
            _tagRepository.CommitAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();

            var tag = _tagRepository.GetByIdAsync(t => t.Id == id && t.IsDeleted==false);

            if (tag == null) return NotFound();

            return View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Tag tag)
        {
            if (!ModelState.IsValid) return View();

            var existTag = await _tagRepository.GetByIdAsync(t => t.Id == tag.Id && t.IsDeleted == false);
            if (existTag == null) return NotFound();

          

            existTag.Name = tag.Name;
            _tagService.UpdateAsync(tag);
            _tagRepository.CommitAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            Tag tag = await _tagRepository.GetByIdAsync(t => t.Id == id && t.IsDeleted==false);
            if (tag == null) return NotFound();

            _tagService.Delete(tag.Id);
            _tagRepository.CommitAsync();
            return Ok();
        }

      
    }
}
