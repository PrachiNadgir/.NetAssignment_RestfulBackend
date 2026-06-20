using Application.DTOs;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagementAPI.Controllers;
using Xunit;

namespace ProductManagementAPI.Tests;

public class ProductControllerTests
{
    private readonly Mock<IProductService>
        _serviceMock;

    private readonly ProductController
        _controller;

    public ProductControllerTests()
    {
        _serviceMock =
            new Mock<IProductService>();

        _controller =
            new ProductController(
                _serviceMock.Object);
    }

    [Fact]
    public async Task GetAll_Should_Return_Ok()
    {
        var products =
            new List<ProductResponseDto>
            {
                new()
                {
                    Id = 1,
                    ProductName = "Laptop"
                }
            };

        _serviceMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(products);

        var result =
            await _controller.GetAll();

        result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_Should_Return_Ok()
    {
        var product =
            new ProductResponseDto
            {
                Id = 1,
                ProductName = "Phone"
            };

        _serviceMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(product);

        var result =
            await _controller.GetById(1);

        result.Should()
            .BeOfType<OkObjectResult>();
    }
}