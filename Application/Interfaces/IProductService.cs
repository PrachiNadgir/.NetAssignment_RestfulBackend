using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>>
        GetAllAsync();

    Task<ProductResponseDto?>
        GetByIdAsync(int id);

    Task<ProductResponseDto>
        CreateAsync(CreateProductDto dto);

    Task UpdateAsync(
        int id,
        UpdateProductDto dto);

    Task DeleteAsync(int id);
}