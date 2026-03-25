using Fiorello.Services.Interfeices;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService settingService;

        public FooterViewComponent(ISettingService setting)
        {
            settingService = setting;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = await settingService.GetAllSettings();
            return View(datas);
        }
    }
}
