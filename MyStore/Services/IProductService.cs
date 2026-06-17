using MyStore.DTOs;
using MyStore.Entities;

namespace MyStore.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task CreateAsync(ProductDto dto);

        Task UpdateAsync(int id, ProductDto dto);

        Task DeleteAsync(int id);
    }
}