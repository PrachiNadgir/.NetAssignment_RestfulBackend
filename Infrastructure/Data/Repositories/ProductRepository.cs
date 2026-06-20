using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProductRepository
    : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>>
        GetAllAsync()
    {
        return await _context.Products
            .Include(x => x.Items)
            .ToListAsync();
    }

    public async Task<Product?>
        GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
}