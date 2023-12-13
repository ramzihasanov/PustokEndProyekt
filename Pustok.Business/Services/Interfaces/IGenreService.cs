using WebApplication6.Models;

namespace WebApplication6.Services.Interfaces
{
    public interface IGenreService
    {
        Task CreateAsync(Genre entity);
        Task Delete(int id);
        Task<Genre> GetByIdAsync(int id);
        Task<List<Genre>> GetAllAsync();
        Task UpdateAsync(Genre genre);
    }
}
