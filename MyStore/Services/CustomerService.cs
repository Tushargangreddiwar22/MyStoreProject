using MyStore.DTOs;
using MyStore.Entities;
using MyStore.Repositories.Interfaces;
using MyStore.Services.Interfaces;

namespace MyStore.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(
            ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CreateAsync(CustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _repo.AddAsync(customer);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(
            int id,
            CustomerDto dto)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
                throw new Exception("Customer Not Found");

            customer.Name = dto.Name;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            _repo.Update(customer);

            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
                throw new Exception("Customer Not Found");

            _repo.Delete(customer);

            await _repo.SaveAsync();
        }
    }
}