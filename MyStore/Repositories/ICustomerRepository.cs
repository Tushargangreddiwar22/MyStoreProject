using MyStore.Entities;

namespace MyStore.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);

        Task AddAsync(Customer customer);

        void Update(Customer customer);

        void Delete(Customer customer);

        Task SaveAsync();
    }
}