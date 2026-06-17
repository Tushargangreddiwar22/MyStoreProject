using Microsoft.AspNetCore.Mvc;
using MyStore.DTOs;
using MyStore.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(
        IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAllAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
            CreateOrderDto dto)
    {
        var orderId =
            await _service.CreateOrderAsync(dto);

        return Ok(new
        {
            OrderId = orderId,
            Message = "Order Created"
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var order =
            await _service.GetOrderAsync(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteOrderAsync(id);
        return Ok("Order Deleted");
    }
}