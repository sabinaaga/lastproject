using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services;
using Fiorello.Services.Interfeices;
using Fiorello.ViewModels;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext appDbContext;    
        private readonly ISliderService _sliderService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public HomeController(AppDbContext dbContext,
            ISliderService sliderService,
            IBlogService blogService,
            ICategoryService categoryService,
            IProductService productService)
        {
                
            appDbContext = dbContext;
            _sliderService = sliderService;
            _blogService = blogService;
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task<IActionResult>  Index()
        {
            SliderVM sliders = await _sliderService.GetAllSlider();
            List<BlogVM> blogs = await _blogService.GetAllBlogs(3);
        
            List<CategoryVM> categories = await _categoryService.GetAllCategoryVMsAsync();
            List<ProductVM> products = await _productService.GetAllProductsAsync();


            HomeVM homeVM = new HomeVM()
            {
                Sliders= sliders,
                Blogs = blogs,
                Categories = categories,
                Products = products,
            };  

            return View(homeVM);
        }
    }
}
