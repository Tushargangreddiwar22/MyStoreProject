using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTOs;
using MyStore.Services.Interfaces;

namespace MyStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(
            ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer =
                await _service.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CustomerDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok("Customer Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CustomerDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok("Customer Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Customer Deleted Successfully");
        }
    }
}