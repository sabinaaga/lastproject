using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Fiorello.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public List<IFormFile> UploadImages { get; set; }
    }
}
