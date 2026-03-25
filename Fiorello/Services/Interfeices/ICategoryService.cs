using Fiorello.ViewModels;

namespace Fiorello.Services.Interfeices
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllCategoryVMsAsync();
    }
}
