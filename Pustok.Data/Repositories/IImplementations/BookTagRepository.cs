using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication6.DAL;

using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;

namespace WebApplication6.Repositories.IImplementations
{
    public class BookTagRepository : GenericRepository<BookTag>, IBookTagRepository
    {
        public BookTagRepository(AppDbContext context) : base(context)
        {
        }

      
    }
}
