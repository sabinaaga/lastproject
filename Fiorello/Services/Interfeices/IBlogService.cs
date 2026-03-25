using Fiorello.ViewModels;

namespace Fiorello.Services.Interfeices
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllBlogs(int? take=null);
        Task<BlogVM> GetBlogByIdAsync(int id);
    }
}
