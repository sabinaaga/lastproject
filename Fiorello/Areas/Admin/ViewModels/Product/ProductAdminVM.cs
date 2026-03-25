using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiorello.Areas.Admin.ViewModels.Product
{
    public class ProductAdminVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductImageVM> ProductImages { get; set; }

        public bool IsMain { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

    }
}
