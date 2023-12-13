using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.CustomExceptions.SliderExceptions;
using WebApplication6.Models;
using WebApplication6.Repositories;
using WebApplication6.Services.Interfaces;

namespace WebApplication6.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAllSAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View(slider);

            try
            {
                await _sliderService.CreateAsync(slider);
            }
            catch (InvalidContentType ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch (InvalidImgSize ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch (InvalidImg ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return View();

            Slider slide = await _sliderService.GetAsync(id);

            if (slide == null) return NotFound();

            return View(slide);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Slider slide)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _sliderService.UpdateAsync(slide);
            }
            catch (InvalidContentType ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch (InvalidImgSize ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch (InvalidImg ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch (InvalidNullReferance)
            {

            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            try
            {
                await _sliderService.DeleteAsync(id);
            }
            catch (InvalidNullReferance)
            {

            }

            return Ok();
        }

    }
}
