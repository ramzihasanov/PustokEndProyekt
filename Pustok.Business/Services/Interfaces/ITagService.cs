using WebApplication6.Models;

namespace WebApplication6.Services.Interfaces
{
    public interface ITagService
    {
        Task CreateAsync(Tag entity);
        Task Delete(int id);
        Task<Tag> GetByIdAsync(int id);
        Task<List<Tag>> GetAllAsync();
        Task UpdateAsync(Tag tag);
    }
}
