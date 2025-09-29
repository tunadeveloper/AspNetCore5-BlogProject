using BlogProject.BusinessLayer.Abstract;
using BlogProject.BusinessLayer.ValidationRules;
using BlogProject.EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly WriterValidator _writerValidator;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;

        public RegisterController(IWriterService writerService, WriterValidator writerValidator, UserManager<IdentityUser<int>> userManager, SignInManager<IdentityUser<int>> signInManager)
        {
            _writerService = writerService;
            _writerValidator = writerValidator;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(Writer writer)
        {
            ValidationResult results = _writerValidator.Validate(writer);
            if (results.IsValid)
            {
                var identityUser = new IdentityUser<int>
                {
                    UserName = writer.WriterEmail,
                    Email = writer.WriterEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(identityUser, writer.WriterPassword);
                
                if (result.Succeeded)
                {
                    writer.WriterStatus = true;
                    writer.WriterImage = null;
                    writer.WriterAbout = null;
                    _writerService.InsertBL(writer);

                    await _signInManager.SignInAsync(identityUser, false);
                    
                    ViewBag.Success = true;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}

