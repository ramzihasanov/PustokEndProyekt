using Microsoft.EntityFrameworkCore;
using WebApplication6.DAL;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;

namespace WebApplication6.Repositories.IImplementations
{
    public class BookRepository : GenericRepository<Book>,IBookRepository
    {
      

        public BookRepository(AppDbContext context) : base(context)
        {
           
        }

    }
}
