using MyStore.DTOs;
using MyStore.Entities;
using MyStore.Repositories.Interfaces;
using MyStore.Services.Interfaces;

namespace MyStore.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repo;

        public SupplierService(ISupplierRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CreateAsync(SupplierDto dto)
        {
            var supplier = new Supplier
            {
                SupplierName = dto.SupplierName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _repo.AddAsync(supplier);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(int id, SupplierDto dto)
        {
            var supplier = await _repo.GetByIdAsync(id);

            if (supplier == null)
                throw new Exception("Supplier Not Found");

            supplier.SupplierName = dto.SupplierName;
            supplier.Email = dto.Email;
            supplier.Phone = dto.Phone;
            supplier.Address = dto.Address;

            _repo.Update(supplier);

            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = await _repo.GetByIdAsync(id);

            if (supplier == null)
                throw new Exception("Supplier Not Found");

            _repo.Delete(supplier);

            await _repo.SaveAsync();
        }
    }
}