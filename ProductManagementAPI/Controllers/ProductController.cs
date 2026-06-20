using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(
        IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult>
        GetAll()
    {
        return Ok(
            await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetById(int id)
    {
        return Ok(
            await _service.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult>
        Create(CreateProductDto dto)
    {
        return Ok(
            await _service.CreateAsync(dto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult>
        Update(
            int id,
            UpdateProductDto dto)
    {
        await _service.UpdateAsync(
            id,
            dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>
        Delete(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}