using BlogProject.BusinessLayer.Abstract;
using BlogProject.BusinessLayer.ValidationRules;
using BlogProject.DataAccessLayer.Abstract;
using BlogProject.DTOLayer.WriterDtos;
using BlogProject.EntityLayer.Concrete;
using BlogProject.PresentationLayer.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly WriterPasswordUpdateValidator _writerValidator;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public WriterController(IWriterService writerService, WriterPasswordUpdateValidator writerValidator, UserManager<IdentityUser<int>> userManager)
        {
            _writerService = writerService;
            _writerValidator = writerValidator;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            return View();
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
        public async Task<IActionResult> WriterProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            Writer currentWriter = null;
            
            if (user != null)
            {
                currentWriter = _writerService.GetByIdBL(user.Id);
            }
            else
            {
                var userMail = User.Identity.Name;
                currentWriter = _writerService.List(x => x.WriterEmail == userMail).FirstOrDefault();
            }

            if (currentWriter == null)
            {
                return RedirectToAction("Index", "Login");
            }

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
        public async Task<IActionResult> WriterProfile(WriterPasswordUpdateDto model)
        {
            var validator = new WriterPasswordUpdateValidator();
            var result = validator.Validate(model);

            var user = await _userManager.GetUserAsync(User);
            Writer currentWriter = null;
            
            if (user != null)
            {
                currentWriter = _writerService.GetByIdBL(user.Id);
            }
            else
            {
                currentWriter = _writerService.List(x => x.WriterEmail == User.Identity.Name).FirstOrDefault();
            }

            if (currentWriter == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (user != null && !string.IsNullOrEmpty(model.OldPassword))
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                if (!passwordCheck)
                {
                    ModelState.AddModelError("OldPassword", "Eski şifre yanlış");
                }
            }
            else if (user == null && !string.IsNullOrEmpty(model.OldPassword) && model.OldPassword != currentWriter.WriterPassword)
            {
                ModelState.AddModelError("OldPassword", "Eski şifre yanlış");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                currentWriter.WriterName = model.WriterName;
                currentWriter.WriterEmail = model.WriterEmail;
                currentWriter.WriterAbout = model.WriterAbout;
                currentWriter.WriterImage = model.WriterImage;

                if (user != null && !string.IsNullOrEmpty(model.NewPassword))
                {
                    await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                }
                else if (user == null && !string.IsNullOrEmpty(model.NewPassword))
                {
                    currentWriter.WriterPassword = model.NewPassword;
                }

                currentWriter.WriterStatus = true;
                _writerService.UpdateBL(currentWriter);
                
                if (user != null)
                {
                    user.Email = model.WriterEmail;
                    user.UserName = model.WriterEmail;
                    await _userManager.UpdateAsync(user);
                }
            }

            return View();
        }

    }
}
