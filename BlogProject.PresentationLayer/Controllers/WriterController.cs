using BlogProject.BusinessLayer.Abstract;
using BlogProject.BusinessLayer.ValidationRules;
using BlogProject.DataAccessLayer.Abstract;
using BlogProject.DTOLayer.WriterDtos;
using BlogProject.EntityLayer.Concrete;
using BlogProject.PresentationLayer.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly WriterPasswordUpdateValidator _writerValidator;

        public WriterController(IWriterService writerService, WriterPasswordUpdateValidator writerValidator)
        {
            _writerService = writerService;
            _writerValidator = writerValidator;
        }

        public IActionResult _WriterLayout()
        {
            return View();
        }

        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }

        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }

        public PartialViewResult ScriptPartial()
        {
            return PartialView();
        }

        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }
        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        [Authorize]
        [HttpGet]
        public IActionResult WriterProfile()
        {
            var userMail = User.Identity.Name;
            var currentWriter = _writerService.List(x => x.WriterEmail == userMail).FirstOrDefault();

            var model = new WriterPasswordUpdateDto
            {
                WriterId = currentWriter.WriterId,
                WriterName = currentWriter.WriterName,
                WriterEmail = currentWriter.WriterEmail,
                WriterAbout = currentWriter.WriterAbout,
                WriterImage = currentWriter.WriterImage
            };

            return View(model);
        }



        [HttpPost]
        public IActionResult WriterProfile(WriterPasswordUpdateDto model)
        {
            var validator = new WriterPasswordUpdateValidator();
            var result = validator.Validate(model);

            var currentWriter = _writerService.List(x => x.WriterEmail == User.Identity.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(model.OldPassword) && model.OldPassword != currentWriter.WriterPassword)
            {
                ModelState.AddModelError("OldPassword", "Eski şifre yanlış");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            currentWriter.WriterName = model.WriterName;
            currentWriter.WriterEmail = model.WriterEmail;
            currentWriter.WriterAbout = model.WriterAbout;
            currentWriter.WriterImage = model.WriterImage;

            if (!string.IsNullOrEmpty(model.NewPassword))
                currentWriter.WriterPassword = model.NewPassword;

            currentWriter.WriterStatus = true;

            _writerService.UpdateBL(currentWriter);

            return View();
        }

    }
}
