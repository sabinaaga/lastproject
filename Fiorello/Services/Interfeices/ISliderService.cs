using Fiorello.ViewModels;

namespace Fiorello.Services.Interfeices
{
    public interface ISliderService
    {
        Task<SliderVM> GetAllSlider ();
    }
}
