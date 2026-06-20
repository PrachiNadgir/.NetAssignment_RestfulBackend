using Application.Mapping;
using Application.Validators;

using FluentValidation;

using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Infrastructure.Logging;

using Application.Interfaces;
using Application.Services;

using Microsoft.EntityFrameworkCore;

using ProductManagementAPI.Extensions;
using ProductManagementAPI.Filters;
using ProductManagementAPI.Middleware;

using Serilog;

SerilogConfiguration.ConfigureSerilog();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection"));
});


builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingFilter>();
    options.Filters.Add<ExceptionFilter>();
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();



builder.Services.AddAutoMapper(
    typeof(ProductProfile));



builder.Services
    .AddValidatorsFromAssemblyContaining<
        CreateProductValidator>();


builder.Services.AddScoped<
    IProductRepository,
    ProductRepository>();

builder.Services.AddScoped<
    IProductService,
    ProductService>();

builder.Services.AddScoped<
    IUnitOfWork,
    UnitOfWork>();

builder.Services.AddScoped<
    IAuthService,
    TokenService>();

builder.Services.AddScoped<
    LoggingFilter>();

builder.Services.AddScoped<
    ExceptionFilter>();

builder.Services.AddJwtAuthentication(
    builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db =
        scope.ServiceProvider
             .GetRequiredService<ApplicationDbContext>();

    db.Database.Migrate();
}

app.Run();