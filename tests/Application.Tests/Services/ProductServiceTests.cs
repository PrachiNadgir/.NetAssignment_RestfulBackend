using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;


namespace Application.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _repositoryMock =
            new Mock<IProductRepository>();

        _unitOfWorkMock =
            new Mock<IUnitOfWork>();

        _mapperMock =
            new Mock<IMapper>();

        _service =
            new ProductService(
                _repositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object);
    }

    [Fact]
    public async Task GetAll_Should_Return_Products()
    {
        var products =
            new List<Product>
            {
                new()
                {
                    Id = 1,
                    ProductName = "Laptop"
                }
            };

        var response =
            new List<ProductResponseDto>
            {
                new()
                {
                    Id = 1,
                    ProductName = "Laptop"
                }
            };

        _repositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(products);

        _mapperMock
            .Setup(x => x.Map<IEnumerable<ProductResponseDto>>(products))
            .Returns(response);

        var result =
            await _service.GetAllAsync();

        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task Create_Should_Create_Product()
    {
        var dto =
            new CreateProductDto
            {
                ProductName = "Phone"
            };

        var product =
            new Product
            {
                ProductName = "Phone"
            };

        _mapperMock
            .Setup(x => x.Map<Product>(
                It.IsAny<CreateProductDto>()))
            .Returns(product);

        _mapperMock
            .Setup(x => x.Map<ProductResponseDto>(
                It.IsAny<Product>()))
            .Returns(new ProductResponseDto
            {
                ProductName = "Phone"
            });

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var result =
            await _service.CreateAsync(dto);

        result.ProductName
            .Should()
            .Be("Phone");
    }

    [Fact]
    public async Task Delete_Should_Remove_Product()
    {
        var product =
            new Product
            {
                Id = 1,
                ProductName = "Laptop"
            };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(product);

        await _service.DeleteAsync(1);

        _repositoryMock.Verify(
            x => x.Delete(product),
            Times.Once);
    }
}