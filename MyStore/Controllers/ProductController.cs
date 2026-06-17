using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTOs;
using MyStore.Services;

namespace MyStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(
            IProductService service)
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
            var product =
                await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            ProductDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok("Product Created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            ProductDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok("Product Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Product Deleted");
        }
    }
}