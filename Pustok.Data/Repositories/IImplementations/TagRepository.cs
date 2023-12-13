using WebApplication6.DAL;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;

namespace WebApplication6.Repositories.IImplementations
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
