using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public IActionResult Index()
        {
            var values = _aboutService.GetAllBL().FirstOrDefault();
            return View(values);
        }

        public PartialViewResult SocialMediaPartial()
        {
            return PartialView();
        }
    }
}
