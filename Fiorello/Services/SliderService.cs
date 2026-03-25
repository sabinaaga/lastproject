using Microsoft.EntityFrameworkCore;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfeices;
using Fiorello.ViewModels;

namespace Fiorello.Services
{
    public class SliderService : ISliderService

    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext appDbContext)
        {
             
            _context = appDbContext;
        }
        public async Task<SliderVM> GetAllSlider()
        {
            List<string> image=await _context.Sliders.Select(n=>n.ImageUrl).ToListAsync();
            SliderInfo sliderInfo=await _context.SliderInfos.FirstOrDefaultAsync();
            return new SliderVM
            
                {
                    ImageUrl=image,
                    Title=sliderInfo.Title,
                    Description=sliderInfo.Description
                };
        }
    }
}
