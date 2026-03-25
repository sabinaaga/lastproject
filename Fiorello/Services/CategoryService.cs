using Microsoft.EntityFrameworkCore;
using Fiorello.Data;
using Fiorello.Services.Interfeices;
using Fiorello.ViewModels;

namespace Fiorello.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext appDbContext)
        {
            _context = appDbContext;         
        }
        public async Task<List<CategoryVM>> GetAllCategoryVMsAsync()
        {
            List<CategoryVM> categoryVMs = await _context.Categories.Include(c => c.Products)
                .Select(c => new CategoryVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    HasProduct = c.Products.Any()
                })
                .ToListAsync();

            return categoryVMs;
        }

       
    }
}
