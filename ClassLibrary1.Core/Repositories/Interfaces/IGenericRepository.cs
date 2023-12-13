using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication6.Models;

namespace WebApplication6.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity>where TEntity : BaseEntity,new()
    {
        public DbSet<TEntity> Table { get; }

        Task CreateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes);

        Task<int> CommitAsync();
    }
}
