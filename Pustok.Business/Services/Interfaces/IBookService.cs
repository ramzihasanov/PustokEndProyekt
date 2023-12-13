using System.Linq.Expressions;
using WebApplication6.Models;

namespace WebApplication6.Services.Interfaces
{
    public interface IBookService
    {
        Task CreateAsync(Book entity);
        Task SoftDelete(int id);
        Task Delete(int id);
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null);
        Task<List<Book>> GetAllRelatedBooksAsync(Book book);
        Task UpdateAsync(Book entity);
    }
}
