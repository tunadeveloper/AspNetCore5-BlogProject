using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public IActionResult Index(int page = 1)
        {
            var values = _newsletterService.GetAllBL().ToPagedList(page, 10);
            return View(values);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, bool status)
        {
            var newsletter = _newsletterService.GetByIdBL(id);
            if (newsletter == null)
                return Json(new { success = false });

            newsletter.EmailStaus = status;
            _newsletterService.UpdateBL(newsletter);

            return Json(new { success = true, newStatus = status });
        }

        [HttpPost]
        public IActionResult DeleteNewsletter(int id)
        {
            var value = _newsletterService.GetByIdBL(id);
            if (value != null)
            {
                _newsletterService.DeleteBL(value);
                return Json(new { success = true, message = "Bülten başarıyla silindi." });
            }
            return Json(new { success = false, message = "Bülten bulunamadı." });
        }
    }
}
