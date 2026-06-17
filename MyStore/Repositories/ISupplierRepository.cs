using MyStore.Entities;

namespace MyStore.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync();

        Task<Supplier?> GetByIdAsync(int id);

        Task AddAsync(Supplier supplier);

        void Update(Supplier supplier);

        void Delete(Supplier supplier);

        Task SaveAsync();
    }
}