using Microsoft.AspNetCore.Mvc;
using Fiorello.Services.Interfeices;

namespace Fiorello.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;

        public ProductController(IProductService product)
        {
           
            _product = product;
        }
        public async Task<IActionResult> Index(int? id)
        {
           var product =await _product.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return View(product);
        }
    }
}
