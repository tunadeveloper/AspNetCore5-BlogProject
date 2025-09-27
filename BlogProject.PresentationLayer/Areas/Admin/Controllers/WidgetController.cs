using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WidgetController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
