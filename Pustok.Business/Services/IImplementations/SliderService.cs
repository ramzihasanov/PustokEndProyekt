using Microsoft.AspNetCore.Hosting;
using WebApplication6.CustomExceptions.SliderExceptions;
using WebApplication6.Helpers;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;
using WebApplication6.Services.Interfaces;

namespace WebApplication6.Services.IImplementations
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IWebHostEnvironment _env;

        public SliderService(ISliderRepository sliderRepository,IWebHostEnvironment env)
        {
            _sliderRepository = sliderRepository;
            _env = env;
        }
        public async Task CreateAsync(Slider slider)
        {
            if (slider.formFile != null)
            {

                if (slider.formFile.ContentType != "image/png" && slider.formFile.ContentType != "image/jpeg")
                {
                    throw new InvalidContentType("Image", "ancaq sekil yukleye bilirsen davay sekil tap gel");
                }

                if (slider.formFile.Length > 1048576)
                {
                    throw new InvalidImgSize("Image", "1 mb dan az sekil tap gel");
                }
            }
            else
            {
                throw new InvalidImg("Image", "sekil deyil bu ");
            }

            string folder = "uploads";

            string newFileName = Helper.GetFileName(_env.WebRootPath, folder, slider.formFile);

            slider.ImageUrl = newFileName;

            await _sliderRepository.CreateAsync(slider);
            await _sliderRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id == null) throw new InvalidNullReferance();

            Slider wantedSlide = await _sliderRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (wantedSlide == null) throw new InvalidNullReferance();

            var path = Path.Combine(_env.WebRootPath, "upload", wantedSlide.ImageUrl);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

             _sliderRepository.DeleteAsync(wantedSlide);
            await _sliderRepository.CommitAsync();
        }

        public async Task<Slider> GetAsync(int id)
        {
            return await _sliderRepository.GetByIdAsync(x=>x.Id==id && x.IsDeleted==false);
        }

        public async Task<List<Slider>> GetAllSAsync()
        {
            return await _sliderRepository.GetAllAsync();
        }

        public async Task UpdateAsync(Slider slider)
        {
            Slider ExistesSlide = await _sliderRepository.GetByIdAsync(x => x.Id == slider.Id && x.IsDeleted == false);

            if (ExistesSlide == null) throw new InvalidNullReferance();

            if (slider.formFile != null)
            {

                if (slider.formFile.ContentType != "image/png" && slider.formFile.ContentType != "image/jpeg")
                {
                    throw new InvalidContentType("Image", "ancaq sekil yukleye bilirsen davay sekil tap gel");
                }

                if (slider.formFile.Length > 1048576)
                {
                    throw new InvalidImgSize("Image", "1 mb dan az sekil tap gel");
                }

             

                string fileNmae = Helper.GetFileName(_env.WebRootPath, "upload", slider.formFile);

                string path = Path.Combine(_env.WebRootPath, "upload", ExistesSlide.ImageUrl);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                ExistesSlide.ImageUrl = fileNmae;
            }

            ExistesSlide.Title = slider.Title;
            ExistesSlide.Descirption = slider.Descirption;
            ExistesSlide.RedirectorUrlText = slider.RedirectorUrlText;
            ExistesSlide.RedirectorUrl = slider.RedirectorUrl;


            await _sliderRepository.CommitAsync();
        }
    }
    
}
