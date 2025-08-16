using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.ViewComponents
{
    public class _HeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
