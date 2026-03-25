using Fiorello.Models;

namespace Fiorello.ViewModels
{
    public class HomeVM
    {
        public SliderVM Sliders { get; set; }
        public List<BlogVM> Blogs { get; set; }
        public List<CategoryVM> Categories { get; set; }  
        public List<ProductVM> Products { get; set; }
        

    }
}
