using MyStore.DTOs;
using MyStore.Entities;

namespace MyStore.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetAllAsync();

        Task<Supplier?> GetByIdAsync(int id);

        Task CreateAsync(SupplierDto dto);

        Task UpdateAsync(int id, SupplierDto dto);

        Task DeleteAsync(int id);
    }
}