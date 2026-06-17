using MyStore.Entities;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);

    Task<Order?> GetOrderAsync(int id);

    Task<List<Order>> GetAllAsync();

    Task DeleteOrderAsync(int id);

    Task SaveAsync();
}