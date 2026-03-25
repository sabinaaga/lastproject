using Fiorello.ViewModels;

namespace Fiorello.Services.Interfeices
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllProductsAsync();
        Task<ProductDetailVM> GetProductByIdAsync(int? id);
    }
}
