using Fiorello.ViewModels;

namespace Fiorello.Areas.Admin.ViewModels.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
         public List<IFormFile> Images { get; set; }
        public List<ProductImageVM> ProductImages { get; set; }
    }
}
