
using WebApplication6.DAL;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;

namespace WebApplication6.Repositories.IImplementations
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
        }
    }
}
