using WebApplication6.DAL;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;

namespace WebApplication6.Repositories.IImplementations
{
    public class BookImageRepository : GenericRepository<BookImage>, IBookImageRepository
    {
        public BookImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
