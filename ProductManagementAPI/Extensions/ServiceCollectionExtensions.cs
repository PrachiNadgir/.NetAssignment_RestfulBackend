using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProductManagementAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection
        AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<
            ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        "DefaultConnection"));
            });

        services.AddScoped<
            IProductRepository,
            ProductRepository>();

        services.AddScoped<
            IProductService,
            ProductService>();

        services.AddScoped<
            IUnitOfWork,
            UnitOfWork>();

        services.AddScoped<
            IAuthService,
            TokenService>();

        services.AddAutoMapper(
            typeof(ProductProfile));

        services.AddValidatorsFromAssemblyContaining<
            CreateProductValidator>();

        return services;
    }
}