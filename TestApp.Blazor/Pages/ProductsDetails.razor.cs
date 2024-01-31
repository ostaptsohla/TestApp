using Microsoft.AspNetCore.Components;
using TestApp.Contracts.Grpc;
using TestApp.Contracts.Models;
using TestApp.Contracts.Models.Requests;

namespace TestApp.Blazor.Pages;

[Route("products/{id:int}")]
[Route("products/new")]
public partial class ProductsDetails
{
    [Parameter]
    public int? Id { get; set; }
    
    private ProductPc Product { get; set; } = new();
    
    [Inject]
    private IProductsGrpcContract _productsGrpcContract { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (Id.HasValue)
        {
            Product = await _productsGrpcContract.GetByIdAsync(new GetProductByIdPcRequest()
            {
                Id = Id!.Value
            });
        }
    }
    
    private async Task Create()
    {
        await _productsGrpcContract.AddAsync(Product);
        NavigateToProducts();
    }

    private async Task Update()
    {
        await _productsGrpcContract.UpdateAsync(Product);
        NavigateToProducts();
    }
    
    private void Cancel()
    {
        NavigateToProducts();
    }
    
    private void NavigateToProducts()
    {
        _navigationManager.NavigateTo("");
    }
}