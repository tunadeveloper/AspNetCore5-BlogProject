using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public LoginController(IWriterService writerService, SignInManager<IdentityUser<int>> signInManager, UserManager<IdentityUser<int>> userManager)
        {
            _writerService = writerService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Writer writer)
        {
            var identityUser = await _userManager.FindByEmailAsync(writer.WriterEmail);
            if (identityUser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(identityUser, writer.WriterPassword, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Widget", new { area = "Admin" });
                }
            }

            var values = _writerService.GetAllBL().FirstOrDefault(x=>x.WriterEmail == writer.WriterEmail && x.WriterPassword == writer.WriterPassword);
            if (values != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, writer.WriterEmail),
                    new Claim(ClaimTypes.NameIdentifier, values.WriterId.ToString())
                };
                var userIdentity=new ClaimsIdentity(claims, "WriterAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync("WriterAuth", claimsPrincipal);
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            
            await HttpContext.SignOutAsync("WriterAuth");
            
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> WriterLogOut()
        {
            await _signInManager.SignOutAsync();
            
            await HttpContext.SignOutAsync("WriterAuth");
            
            return RedirectToAction("Index", "Login");
        }

    }
}
