using Microsoft.EntityFrameworkCore;
using Fiorello.Data;
using Fiorello.Services.Interfeices;
using Fiorello.ViewModels;

namespace Fiorello.Services
{
    public class ProductService : IProductService

    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<ProductVM>> GetAllProductsAsync()
        {
            List<ProductVM> products = await _context.Products.Include(n=>n.ProductImages).Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                ProductImages = p.ProductImages.Select(pi => new ProductImageVM
                {
                    Name = pi.ImageUrl,
                    Main = pi.Main

                }).ToList()
            }).ToListAsync();

            return products;
        }

        public async Task<ProductDetailVM> GetProductByIdAsync(int? id)
        {
            var product = await _context.Products.Include(n => n.ProductImages)
                                                 .Include(m=>m.Category)
                                                 .Where(p => p.Id == id).Select(p => new ProductDetailVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                ProductImages = p.ProductImages.Select(pi => new ProductImageVM
                {
                    Name = pi.ImageUrl,
                    Main = pi.Main
                }).ToList()
            }).FirstOrDefaultAsync();
            return product;
        }
    }
}
