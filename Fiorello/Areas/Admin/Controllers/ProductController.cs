using Fiorello.Areas.Admin.ViewModels.Product;
using Fiorello.Areas.Admin.ViewModels.Slider;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfeices;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly AppDbContext appDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;


        public ProductController(AppDbContext appDbContext
                                 , IWebHostEnvironment webHostEnvironment )
        {
            this.appDbContext = appDbContext;
            this.webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {

            var datas = await appDbContext.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .ToListAsync();

            var response = datas.Select(p => new ProductAdminVM
            {
                Id = p.Id,
                Name = p.Name,
                ProductImages = p.ProductImages.Select(pi => new ProductImageVM
                {
                    Id = pi.Id,
                    Name = pi.ImageUrl,
                    Main = pi.Main

                }).ToList(),
                Price = p.Price,
                CategoryName = p.Category.Name
            });  
            return View(response);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = appDbContext.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            ViewBag.categories = await categories;  

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            List<ProductImage> productImages = new();

            var categories = appDbContext.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            ViewBag.categories = await categories;
            if (!ModelState.IsValid) { return View(request);}

            foreach (var item in request.UploadImages )
            {
                string faleName = Guid.NewGuid().ToString() + "_" + item.FileName;

                string fullPath = Path.Combine(webHostEnvironment.WebRootPath, "img", faleName);


                if (!item.ContentType.Contains("image/"))
                {
                    {
                        ModelState.AddModelError("UploadImage", "Input type must be only image");
                        return View(request);
                    }
                }


                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                };


                productImages.Add(new ProductImage
                {
                    ImageUrl = faleName,
                    Main = request.UploadImages.IndexOf(item) == 0 ? true : false
                });

              
            }

            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId,
                ProductImages = productImages
            };

            await appDbContext.Products.AddAsync(product);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            var categories = appDbContext.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            ViewBag.categories = await categories;

            var data= await appDbContext.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
             if (data == null) { return NotFound(); }
             var response = new ProductEditVM
             {
                 Id = data.Id,
                 Name = data.Name,
                 Price = data.Price,
                 CategoryId = data.CategoryId,
                 ProductImages = data.ProductImages.Select(pi => new ProductImageVM
                 {
                     Id = pi.Id,
                     Name = pi.ImageUrl,
                     Main = pi.Main
                 }).ToList()
             };
             return View(response);

            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductInUpdate(int id)
        {
            var image = await appDbContext.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);

            if (image == null) { return NotFound(); }

            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, "img", image.ImageUrl);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        
            appDbContext.ProductImages.Remove(image);
            await appDbContext.SaveChangesAsync();
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> IsMainProductInUpdate(int id)
        {
            var image = await appDbContext.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id );
            if (image == null) { return NotFound(); }

            var productId = image.ProductId;

            var product = await appDbContext.ProductImages.Where(pi => pi.ProductId == productId).ToListAsync();

            foreach (var item in product)
            {
                item.Main=false;
            }

            image.Main = true;
            await appDbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int? id, ProductEditVM request)
        {

            var categories = appDbContext.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            ViewBag.categories = await categories;

            var data = await appDbContext.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            List<ProductImage> productImages = new();


            if (data == null) { return NotFound(); }


            if (request.Images is not null)
            {

                foreach (var item in request.Images)
            {
                string faleName = Guid.NewGuid().ToString() + "_" + item.FileName;

                string fullPath = Path.Combine(webHostEnvironment.WebRootPath, "img", faleName);


                if (!item.ContentType.Contains("image/"))
                {
                    {
                        ModelState.AddModelError("UploadImage", "Input type must be only image");
                        return View(request);
                    }
                }


                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
                ;


                productImages.Add(new ProductImage
                {
                    ImageUrl = faleName,
                   
                });


            }
            }
            data.Name = request.Name;
            data.Price = request.Price;
            data.CategoryId = request.CategoryId;
            data.ProductImages.AddRange(productImages);

            await appDbContext.SaveChangesAsync();


            return RedirectToAction(nameof(Index));


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id)
        {
            var data = await appDbContext.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
                if (data == null) { return NotFound(); }



                foreach (var item in data.ProductImages)
                {
                    string fullPath = Path.Combine(webHostEnvironment.WebRootPath, "img", item.ImageUrl);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
            }
 
            

            appDbContext.Products.Remove(data);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




    }
}

