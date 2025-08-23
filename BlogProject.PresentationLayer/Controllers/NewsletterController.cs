using BlogProject.BusinessLayer.Abstract;
using BlogProject.BusinessLayer.ValidationRules;
using BlogProject.EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletterService;
        private readonly NewsletterValidator _newsletterValidator;

        public NewsletterController(INewsletterService newsletterService, NewsletterValidator newsletterValidator)
        {
            _newsletterService = newsletterService;
            _newsletterValidator = newsletterValidator;
        }


        [HttpPost]
        public IActionResult Subscribe(Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                newsletter.EmailStaus = true;
                _newsletterService.InsertBL(newsletter);
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors });
            }
        }

        public PartialViewResult FooterNewsletter()
        {
            return PartialView();
        }

    }
}
