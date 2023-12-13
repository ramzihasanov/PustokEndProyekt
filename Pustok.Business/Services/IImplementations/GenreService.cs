using Pustok.Business.CustomExceptions.GenreException;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;
using WebApplication6.Services.Interfaces;

namespace WebApplication6.Services.IImplementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task CreateAsync(Genre entity)
        {
            if (_genreRepository.Table.Any(x => x.Name == entity.Name)) 
            throw new InvalidNameException("GenreId","this name has data , please  insert other name ");
            await _genreRepository.CreateAsync(entity);
            await _genreRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _genreRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);
            if (entity == null) throw new NotFoundDataException("GenreId","genre id is not found");
            entity.IsDeleted = true;
            await _genreRepository.CommitAsync();
        }

        public async Task<List<Genre>> GetAllAsync()
        {
         return  await _genreRepository.GetAllAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            var entity =await _genreRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);
            if (entity is null) throw new NotFoundDataException("GenreId", "genre id is not found");
            return entity;
        }

        public async Task UpdateAsync(Genre genre)
        {
            var existEntity=await _genreRepository.GetByIdAsync(x=>x.Id==genre.Id);
            if (existEntity is null) throw new NotFoundDataException("GenreId", "genre id is not found");
            existEntity.Name=genre.Name;
            await _genreRepository.CommitAsync();
        }
    }
}
