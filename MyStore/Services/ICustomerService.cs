using MyStore.DTOs;
using MyStore.Entities;

namespace MyStore.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);

        Task CreateAsync(CustomerDto dto);

        Task UpdateAsync(int id, CustomerDto dto);

        Task DeleteAsync(int id);
    }
}