using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IWriterService _writerService;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public DashboardController(IBlogService blogService, IWriterService writerService, UserManager<IdentityUser<int>> userManager)
        {
            _blogService = blogService;
            _writerService = writerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.allBlogCount = _blogService.GetAllBL().Count;

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                int writerId = user.Id;
                ViewBag.myBlogCount = _blogService.GetBlogListByWriterBL(writerId).Count;
            }
            else
            {
                var userMail = User.Identity.Name;
                var writer = _writerService.List(x => x.WriterEmail == userMail).FirstOrDefault();
                var writerId = writer != null ? writer.WriterId : 0;
                ViewBag.myBlogCount = _blogService.GetBlogListByWriterBL(writerId).Count;
            }

            var lastSevenDays = DateTime.Now.AddDays(-7);
            var now = DateTime.Now;
            ViewBag.blogCountLastSevenDays = _blogService.GetAllBL()
            .Count(x => x.BlogCreateDate >= lastSevenDays && x.BlogCreateDate <= now);

            return View();
        }
    }
}
