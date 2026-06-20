using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class ProductRepositoryTests
{
    private ApplicationDbContext CreateDbContext()
    {
        var options =
            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(
                Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Add_Should_Save_Product()
    {
        var context =
            CreateDbContext();

        var repository =
            new ProductRepository(context);

        var product =
            new Product
            {
                ProductName = "Laptop",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

        await repository.AddAsync(product);

        await context.SaveChangesAsync();

        var result =
            await context.Products
                .FirstOrDefaultAsync();

        result.Should().NotBeNull();

        result!.ProductName
            .Should()
            .Be("Laptop");
    }

    [Fact]
    public async Task GetById_Should_Return_Product()
    {
        var context =
            CreateDbContext();

        var product =
            new Product
            {
                ProductName = "Phone",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            };

        context.Products.Add(product);

        await context.SaveChangesAsync();

        var repository =
            new ProductRepository(context);

        var result =
            await repository.GetByIdAsync(
                product.Id);

        result.Should().NotBeNull();

        result!.ProductName
            .Should()
            .Be("Phone");
    }
}