using Microsoft.EntityFrameworkCore;
using MyStore;
using MyStore.Entities;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        return await _context.Orders
            .Include(x => x.Customer)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(x => x.Customer)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ToListAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order != null)
        {
            _context.Orders.Remove(order);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}