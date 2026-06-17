using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTOs;
using MyStore.Services;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(
        ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories =
            await _service.GetAllAsync();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var category =
            await _service.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CategoryDto dto)
    {
        await _service.CreateAsync(dto);

        return Ok("Category Created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        CategoryDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok("Category Updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        await _service.DeleteAsync(id);

        return Ok("Category Deleted");
    }
}