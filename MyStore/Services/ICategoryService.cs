// If Category and CategoryDto live in a different namespace, update the using directives below.
using System.Collections.Generic;
using System.Threading.Tasks;
using MyStore.DTOs;
using MyStore.Entities;

namespace MyStore.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task CreateAsync(CategoryDto dto);

        Task UpdateAsync(int id, CategoryDto dto);

        Task DeleteAsync(int id);
    }
}
