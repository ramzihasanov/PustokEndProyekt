using Microsoft.EntityFrameworkCore;
using WebApplication6.DAL;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;

namespace WebApplication6.Repositories.IImplementations
{
    public class SliderRepository : GenericRepository<Slider>, ISliderRepository
    {
        public SliderRepository(AppDbContext context) : base(context)
        {
        }
    }
}

