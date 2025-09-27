using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
