using Fiorello.Areas.Admin.ViewModels.Slider;
using Fiorello.Data;
using Fiorello.Helpers.Extentions;
using Fiorello.Models;
using Fiorello.Services.Interfeices;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SliderController : Controller
    {


        private readonly AppDbContext appDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SliderController(AppDbContext appDbContext
                              , IWebHostEnvironment webHost  )
        {
            this.appDbContext = appDbContext;
            webHostEnvironment = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var datas = await appDbContext.Sliders.Select(v => new Fiorello.Areas.Admin.ViewModels.Slider.SliderVM
            {
                Id = v.Id,
                Image = v.ImageUrl
            })

            .ToListAsync();
            return View(datas);
        }
        [HttpGet]

        public async Task<IActionResult> Detail(int id)
        {
            var datas = await appDbContext.Sliders.FirstOrDefaultAsync(v => v.Id == id);

            var sliderVM=new SliderDetailVM
            {
                Id = datas.Id,
                Image = datas.ImageUrl
            };
            return View(sliderVM);

        }


        [HttpGet]

        public async Task<IActionResult> Create()
        {

       
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);

           
            foreach (var item in request.Image)
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
                } ;
                await appDbContext.Sliders.AddAsync(new Slider { ImageUrl = faleName });

            }

            await appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
            {
            var slider = await appDbContext.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, "img", slider.ImageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            appDbContext.Sliders.Remove(slider);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return BadRequest();
            var slider = await appDbContext.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null) return NotFound();


            return View(new SliderEditVM { ExistImage = slider.ImageUrl });


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderEditVM sliderEdit)
        {

            if (id == null) return BadRequest();

            var slider = await appDbContext.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider == null) return NotFound();
             
               if (sliderEdit.UploadImage is not null)
            {

                string faleName = Guid.NewGuid().ToString() + "_" + sliderEdit.UploadImage.FileName;
                string newPath = Path.Combine(webHostEnvironment.WebRootPath, "img", faleName);
                using (FileStream stream = new FileStream(newPath, FileMode.Create))
                {
                    await sliderEdit.UploadImage.CopyToAsync(stream);
                }

                string oldPath = Path.Combine(webHostEnvironment.WebRootPath, "img", slider.ImageUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                if (sliderEdit.UploadImage == null)
                {
                    ModelState.AddModelError("UploadImage", "Please select an image");
                    return View(sliderEdit);
                }



                slider.ImageUrl = faleName;
                await appDbContext.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));


        }

    }
}
