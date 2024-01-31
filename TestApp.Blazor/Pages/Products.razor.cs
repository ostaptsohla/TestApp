using Microsoft.AspNetCore.Components;
using TestApp.Contracts.Grpc;
using TestApp.Contracts.Models;

namespace TestApp.Blazor.Pages;

[Route("")]
[Route("products")]
public partial class Products
{
    private List<ProductPc> ProductsList { get; set; } = new();
    
    [Inject]
    private IProductsGrpcContract _productsGrpcContract { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ProductsList = await _productsGrpcContract.GetProductsAsync();
    }
    
    private void NavigateToDetails(int id)
    {
        _navigationManager.NavigateTo($"products/{id}");
    }
    
    private void Create()
    {
        _navigationManager.NavigateTo("products/new");
    }
}