using Fiorello.Areas.Admin.ViewModels.Category;
using Fiorello.Data;
using Fiorello.Services.Interfeices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        

        private readonly AppDbContext appDbContext;
        private readonly ICategoryService categoryService;

        public CategoryController(AppDbContext appDbContext, ICategoryService categoryService)
        {
            this.appDbContext = appDbContext;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories =await categoryService.GetAllCategoryVMsAsync();
            return View(categories);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CtegoryCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);

           var datas= await appDbContext.Categories.ToListAsync();
            foreach (var item in datas)
            {
                if (request.Name ==item.Name)
                {
                    ModelState.AddModelError("Name", "This category name already exists");
                    return View(request);
                }
            }
            //Second Vay 
            //var exsistData = await appDbContext.Categories.FirstOrDefaultAsync(m => m.Name == request.Name);
            //if (exsistData != null)
            //{
            //    ModelState.AddModelError("Name", ValidationMessace.DataExsist);
            //    return View(request);
            //}


            var data = await appDbContext.Categories.AddAsync(new()
            {
                Name = request.Name
            });
            await appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await appDbContext.Categories.FindAsync(id);
            if (category == null) return NotFound();
            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {

            var category = await appDbContext.Categories.FirstOrDefaultAsync(v => v.Id == id);


            var data = new CategoryDetailVM
            {
                Name = category.Name
            };

            if (category == null) return NotFound();
            return View(data);
        }



        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var datas = await appDbContext.Categories.FindAsync(id);
            if (datas == null) return NotFound();
            var categoryUpdateVM = new CtegoryUpdateVM
            {
                
                Name = datas.Name
            };

            return View(categoryUpdateVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, CtegoryUpdateVM request)
        {
            var datas = await appDbContext.Categories.FindAsync(id);
            if (datas == null) return NotFound();
           

            datas.Name = request.Name;

            await appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            }
    }
}
