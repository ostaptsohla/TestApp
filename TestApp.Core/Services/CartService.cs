using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc;
using TestApp.Contracts.Models;
using TestApp.Contracts.Models.Requests;
using TestApp.Database;
using TestApp.Models;

namespace TestApp.Services;

public interface ICartService
{
    Task<CartPc> CreateAsync(CancellationToken cancellationToken = default);
    Task AddProductToCartAsync(ProductQuantityRequest request, CancellationToken cancellationToken = default);
    Task ChangeProductQuantityAsync(ProductQuantityRequest request, CancellationToken cancellationToken = default);
    Task DeleteProductFromCartAsync(ProductCartRequest request, CancellationToken cancellationToken = default);
    Task<CartPc> GetCartByIdAsync(GetCartByIdPcRequest request, CancellationToken cancellationToken = default);
    Task<List<CartPc>> GetAllCartsAsync(CancellationToken cancellationToken = default);
}

public class CartService : ICartService
{
    private readonly StoreDbContext _context;
    private readonly IProductService _productService;

    public CartService(StoreDbContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }
    public async Task<CartPc> CreateAsync(CancellationToken cancellationToken = default)
    {
        var cart = new Cart();
        _context.Add(cart);
        await _context.SaveChangesAsync(cancellationToken);

        return new CartPc()
        {
            Id = cart.Id,
        };
    }
    public async Task AddProductToCartAsync(ProductQuantityRequest request, CancellationToken cancellationToken = default)
    {
        var cart = await GetCartByIdAsync(new GetCartByIdPcRequest() { Id = request.CartId });
        var product = await _productService.GetByIdAsync(new GetProductByIdPcRequest() { Id = request.ProductId });
        var cartProduct = await _context.CartProducts
            .Where(cp => cp.CartId == cart.Id && cp.ProductId == product.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (cartProduct == null)
        {
            var newCartProduct = new CartProduct()
            {
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = request.Quantity,
            };
            _context.Add(newCartProduct);
        }
        else
        {
            cartProduct.Quantity += request.Quantity;
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task ChangeProductQuantityAsync(ProductQuantityRequest request, CancellationToken cancellationToken = default)
    {
        var cart = await GetCartByIdAsync(new GetCartByIdPcRequest() { Id = request.CartId });
        var product = await _productService.GetByIdAsync(new GetProductByIdPcRequest() { Id = request.ProductId });
        var cartProduct = await _context.CartProducts
            .Where(cp => cp.CartId == cart.Id && cp.ProductId == product.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (cartProduct == null)
        {
            throw new Exception($"Cart with Id + {cart.Id} + doesn't contain product with Id + {product.Id}");
        }
        else
        {
            cartProduct.Quantity = request.Quantity;
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteProductFromCartAsync(ProductCartRequest request, CancellationToken cancellationToken = default)
    {
        var cart = await GetCartByIdAsync(new GetCartByIdPcRequest() { Id = request.CartId });
        var product = await _productService.GetByIdAsync(new GetProductByIdPcRequest() { Id = request.ProductId });
        var cartProduct = await _context.CartProducts
            .Where(cp => cp.CartId == cart.Id && cp.ProductId == product.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (cartProduct == null)
        {
            throw new Exception($"Cart with Id + {cart.Id} + doesn't contain product with Id + {product.Id}");
        }
        else
        {
            _context.Remove(cartProduct);
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<CartPc> GetCartByIdAsync(GetCartByIdPcRequest request, CancellationToken cancellationToken = default)
    {
        var cart = await _context.Carts
            .Where(c => c.Id == request.Id)
            .Include(c => c.CartProducts)
            .ThenInclude(cp => cp.Product)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (cart is null)
        {
            throw new Exception($"Cart not found with Id + {request.Id}");
        }

        return new CartPc()
        {
            Id = cart.Id,
            TotalPrice = cart.CartProducts != null ? cart.CartProducts.Sum(p => p.Quantity * p.Product.Price) : 0,
            CartProducts = cart.CartProducts?.Select(cp => new CartProductPc
            {
                Product = new ProductPc()
                {
                    Id = cp.Product.Id,
                    Name = cp.Product.Name,
                    Description = cp.Product.Description,
                    Price = cp.Product.Price
                },
                Quantity = cp.Quantity
            }).ToList()
        };
    }
    public async Task<List<CartPc>> GetAllCartsAsync(CancellationToken cancellationToken = default)
    {
        var carts = await _context.Carts
            .Include(c => c.CartProducts)
            .ThenInclude(cp => cp.Product)
            .ToListAsync(cancellationToken);

        return carts.Select(c => new CartPc()
        {
            Id = c.Id,
            TotalPrice = c.CartProducts != null ? c.CartProducts.Sum(p => p.Quantity * p.Product.Price) : 0,
            CartProducts = c.CartProducts?.Select(cp => new CartProductPc
            {
                Product = new ProductPc()
                {
                    Id = cp.Product.Id,
                    Name = cp.Product.Name,
                    Description = cp.Product.Description,
                    Price = cp.Product.Price
                },
                Quantity = cp.Quantity
            }).ToList()
        }).ToList();
    }
}
