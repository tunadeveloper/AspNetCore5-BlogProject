using BlogProject.BusinessLayer.Abstract;
using BlogProject.BusinessLayer.ValidationRules;
using BlogProject.EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly WriterValidator _writerValidator;
        public RegisterController(IWriterService writerService, WriterValidator writerValidator)
        {
            _writerService = writerService;
            _writerValidator = writerValidator;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(Writer writer)
        {
            ValidationResult results = _writerValidator.Validate(writer);
            if (results.IsValid)
            {
                writer.WriterStatus = true;
                writer.WriterImage = null;
                writer.WriterAbout = null;
                _writerService.InsertBL(writer);
                ViewBag.Success = true;
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }

        }
    }
}

