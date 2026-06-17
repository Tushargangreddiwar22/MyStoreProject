using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTOs;
using MyStore.Services.Interfaces;

namespace MyStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SuppliersController(ISupplierService service)
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
            var supplier = await _service.GetByIdAsync(id);

            if (supplier == null)
                return NotFound();

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok("Supplier Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            SupplierDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok("Supplier Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Supplier Deleted Successfully");
        }
    }
}