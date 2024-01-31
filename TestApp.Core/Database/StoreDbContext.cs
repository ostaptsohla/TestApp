using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Database;

public class StoreDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public StoreDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        
        base.OnConfiguring(optionsBuilder);
    }
}