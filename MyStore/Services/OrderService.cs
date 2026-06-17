using Microsoft.EntityFrameworkCore;
using MyStore;
using MyStore.DTOs;
using MyStore.Entities;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly IOrderRepository _repo;

    public OrderService(
        ApplicationDbContext context,
        IOrderRepository repo)
    {
        _context = context;
        _repo = repo;
    }

    public async Task<int> CreateOrderAsync(
        CreateOrderDto dto)
    {
        decimal totalAmount = 0;

        var order = new Order
        {
            CustomerId = dto.CustomerId,
            OrderDate = DateTime.Now
        };

        foreach (var item in dto.Items)
        {
            var product =
                await _context.Products
                    .FirstOrDefaultAsync(
                        p => p.Id == item.ProductId);

            if (product == null)
                throw new Exception(
                    $"Product {item.ProductId} not found");

            var itemTotal =
                product.Price * item.Quantity;

            totalAmount += itemTotal;

            order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = itemTotal
                });
        }

        order.TotalAmount = totalAmount;

        await _repo.AddOrderAsync(order);

        await _repo.SaveAsync();

        return order.Id;
    }

    public async Task<Order?> GetOrderAsync(
        int orderId)
    {
        return await _repo.GetOrderAsync(orderId);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _repo.GetOrderAsync(orderId);

        if (order == null)
            throw new Exception("Order not found");

        await _repo.DeleteOrderAsync(orderId);
        await _repo.SaveAsync();
    }
}