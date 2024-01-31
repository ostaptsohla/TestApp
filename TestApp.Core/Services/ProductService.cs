using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc;
using TestApp.Contracts.Models;
using TestApp.Contracts.Models.Requests;
using TestApp.Database;
using TestApp.Models;

namespace TestApp.Services;

public interface IProductService
{
    Task<List<ProductPc>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ProductPc> GetByIdAsync(
        GetProductByIdPcRequest request, CancellationToken cancellationToken = default);
    
    Task AddAsync(ProductPc model, CancellationToken cancellationToken = default);

    Task UpdateAsync(ProductPc model, CancellationToken cancellationToken = default);
}

public class ProductService : IProductService
{
    private readonly StoreDbContext _context;

    public ProductService(StoreDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ProductPc>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _context.Products.ToListAsync(cancellationToken);

        return items.Select(z => new ProductPc()
        {
            Id = z.Id,
            Name = z.Name,
            Description = z.Description,
            Price = z.Price
        }).ToList();
    }
    
    public async Task<ProductPc> GetByIdAsync(
        GetProductByIdPcRequest request, CancellationToken cancellationToken = default)
    {
        var item = await _context
            .Products
            .SingleOrDefaultAsync(z => z.Id == request.Id, cancellationToken);
        if (item is null)
        {
            throw new Exception($"Product not found with Id + {request.Id}");
        }
        
        return new ProductPc()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price
        };
    }
    
    public async Task AddAsync(ProductPc model, CancellationToken cancellationToken = default)
    {
        var itemExist = await _context
            .Products
            .SingleOrDefaultAsync(z => z.Name == model.Name, cancellationToken);
        if (itemExist is not null)
        {
            throw new Exception($"Product already exist with Name + {model.Name}");
        }
        
        var newItem = new Product()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price
        };
        _context.Add(newItem);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(ProductPc model, CancellationToken cancellationToken = default)
    {
        var item = await _context
            .Products
            .SingleOrDefaultAsync(z => z.Id == model.Id, cancellationToken);
        if (item is null)
        {
            throw new Exception($"Product not found with Id + {model.Id}");
        }

        item.Name = model.Name;
        item.Description = model.Description;
        item.Price = model.Price;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}