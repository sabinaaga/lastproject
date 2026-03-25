using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfeices;
using Fiorello.ViewModels;

namespace Fiorello.Controllers
{
    public class BlogController : Controller
    {
       private readonly IBlogService _blogService;
        private readonly ISettingService  settingService;
        public BlogController(IBlogService blogService,
                                   ISettingService settingService)
        {
            _blogService = blogService;
            this.settingService = settingService;
        }
        public async Task< IActionResult> Index()
        {
            var skip = await _blogService.GetAllBlogs();
            ViewBag.Blogs = skip.Count();
            var setting = await settingService.GetAllSettings();
            var settingSkip = int.Parse(setting["Skip"]);

            return View (await _blogService.GetAllBlogs(settingSkip));
        }

        public async Task< IActionResult> Details(int id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            BlogVM? blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        public async Task <IActionResult> ShowMore(int skip)
        {
            var blogs = await _blogService.GetAllBlogs();
            var setting= await settingService.GetAllSettings();
            var take= int .Parse (setting["Take"]);

            var filterBlogs = blogs.Skip(skip).Take(take).ToList();
            return PartialView("_BlogPartial", filterBlogs);
        }
    }
}
