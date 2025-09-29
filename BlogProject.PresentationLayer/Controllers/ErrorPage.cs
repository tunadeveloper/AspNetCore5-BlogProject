using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class ErrorPage : Controller
    {
        public IActionResult Error404(int code)
        {
            return View();
        }
    }
}
