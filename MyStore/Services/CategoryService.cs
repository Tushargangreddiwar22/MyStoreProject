using MyStore.DTOs;
using MyStore.Entities;
using MyStore.Repositories;

namespace MyStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(
            ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(CategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = true
            };

            await _repository.AddAsync(category);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(
            int id,
            CategoryDto dto)
        {
            var category =
                await _repository.GetByIdAsync(id);

            if (category == null)
                throw new Exception("Category not found");

            category.Name = dto.Name;
            category.Description = dto.Description;

            _repository.Update(category);

            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category =
                await _repository.GetByIdAsync(id);

            if (category == null)
                throw new Exception("Category not found");

            _repository.Delete(category);

            await _repository.SaveAsync();
        }
    }
}
