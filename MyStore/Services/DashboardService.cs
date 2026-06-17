using Microsoft.EntityFrameworkCore;
using MyStore;
using MyStore.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        return new DashboardDto
        {
            TotalProducts = await _context.Products.CountAsync(),
            TotalCategories = await _context.Categories.CountAsync(),
            TotalCustomers = await _context.Customers.CountAsync(),
            TotalOrders = await _context.Orders.CountAsync(),

            TotalRevenue = await _context.Orders
                .SumAsync(x => x.TotalAmount)
        };
    }
}