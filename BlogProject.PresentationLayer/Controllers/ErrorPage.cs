using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.Controllers
{
    public class ErrorPage : Controller
    {
        public IActionResult Error404(int code)
        {
            return View();
        }
    }
}
