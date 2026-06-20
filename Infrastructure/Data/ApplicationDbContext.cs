using Domain.Entities;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<RefreshToken> RefreshTokens
    {
        get;
        set;
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(
            new ProductConfiguration());

        modelBuilder.ApplyConfiguration(
            new ItemConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}