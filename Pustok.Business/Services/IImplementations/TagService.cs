using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;
using WebApplication6.Services.Interfaces;

namespace WebApplication6.Services.IImplementations
{
    public class TagService:ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task CreateAsync(Tag tag)
        {
            if (_tagRepository.Table.Any(x => x.Name == tag.Name))
                throw new NullReferenceException();
            await _tagRepository.CreateAsync(tag);
            await _tagRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _tagRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);
            if (entity == null) throw new NullReferenceException();

            _tagRepository.DeleteAsync(entity);
            await _tagRepository.CommitAsync();
        }

        public  async Task<List<Tag>> GetAllAsync()
        {
            return await _tagRepository.GetAllAsync();
        }
           

        public async Task<Tag> GetByIdAsync(int id)
        {
           return await _tagRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted==false);
        }

        public async Task UpdateAsync(Tag tag)
        {
            var gen = await _tagRepository.GetByIdAsync(x => x.Id == tag.Id && x.IsDeleted == false);
            if (gen is null) throw new NullReferenceException();

            if (_tagRepository.Table.Any(x => x.Name == tag.Name && gen.Id != tag.Id))
                throw new NullReferenceException();

            gen.Name = tag.Name;
            _tagRepository.CommitAsync();
        }
    }
}
