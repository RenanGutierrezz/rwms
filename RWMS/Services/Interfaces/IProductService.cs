using RWMS.Models.ViewModels.Product;

namespace RWMS.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductListViewModel>> GetAllProductsAsync(CancellationToken ct = default);
    Task<ProductDetailViewModel> GetProductByIdAsync(int id, CancellationToken ct = default);
    Task CreateProductAsync(CreateProductViewModel model, CancellationToken ct = default);
    Task UpdateProductAsync(int id, EditProductViewModel model, CancellationToken ct = default);
    Task DeactivateProductAsync(int id, CancellationToken ct = default);
}