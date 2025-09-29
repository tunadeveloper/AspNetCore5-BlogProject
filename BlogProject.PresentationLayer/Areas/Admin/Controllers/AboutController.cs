using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public IActionResult Index()
        {
            var values = _aboutService.GetAllBL();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAbout(About about)
        {
            if (ModelState.IsValid)
            {
                about.AboutStatus = true;
                _aboutService.InsertBL(about);
                return RedirectToAction("Index");
            }
            return View(about);
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var value = _aboutService.GetByIdBL(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateAbout(About about)
        {
            if (ModelState.IsValid)
            {
                _aboutService.UpdateBL(about);
                return RedirectToAction("Index");
            }
            return View(about);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, bool status)
        {
            var about = _aboutService.GetByIdBL(id);
            if (about == null)
                return Json(new { success = false });

            about.AboutStatus = status;
            _aboutService.UpdateBL(about);

            return Json(new { success = true, newStatus = status });
        }

        public IActionResult DeleteAbout(int id)
        {
            var value = _aboutService.GetByIdBL(id);
            _aboutService.DeleteBL(value);
            return RedirectToAction("Index");
        }
    }
}
