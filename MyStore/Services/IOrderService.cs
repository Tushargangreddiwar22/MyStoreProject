using MyStore.DTOs;
using MyStore.Entities;

public interface IOrderService
{
    Task<int> CreateOrderAsync(
        CreateOrderDto dto);

    Task<Order?> GetOrderAsync(
        int orderId);

    Task<List<Order>> GetAllAsync();

    Task DeleteOrderAsync(int orderId);
}