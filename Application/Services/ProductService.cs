using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class ProductService
    : IProductService
{
    private readonly IProductRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>>
        GetAllAsync()
    {
        var products =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<ProductResponseDto>>
            (products);
    }

    public async Task<ProductResponseDto?>
        GetByIdAsync(int id)
    {
        var product =
            await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException(
                $"Product {id} not found");
        }

        return _mapper.Map<
            ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto>
        CreateAsync(CreateProductDto dto)
    {
        var product =
            _mapper.Map<Product>(dto);

        product.CreatedBy = "Admin";

        product.CreatedOn = DateTime.UtcNow;

        await _repository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<
            ProductResponseDto>(product);
    }

    public async Task UpdateAsync(
        int id,
        UpdateProductDto dto)
    {
        var product =
            await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException(
                $"Product {id} not found");
        }

        product.ProductName =
            dto.ProductName;

        product.ModifiedBy = "Admin";

        product.ModifiedOn =
            DateTime.UtcNow;

        _repository.Update(product);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product =
            await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException(
                $"Product {id} not found");
        }

        _repository.Delete(product);

        await _unitOfWork.SaveChangesAsync();
    }
}