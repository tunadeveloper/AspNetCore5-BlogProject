using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BlogProject.PresentationLayer.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IWriterService _writerService;

        public DashboardController(IBlogService blogService, IWriterService writerService)
        {
            _blogService = blogService;
            _writerService = writerService;
        }

        public IActionResult Index()
        {
            ViewBag.allBlogCount = _blogService.GetAllBL().Count;

            var userMail = User.Identity.Name;
            var writer = _writerService.List(x => x.WriterEmail == userMail).FirstOrDefault();
            var writerId = writer != null ? writer.WriterId : 0;

            ViewBag.myBlogCount = _blogService.GetBlogListByWriterBL(writerId).Count;

            var lastSevenDays = DateTime.Now.AddDays(-7);
            var now = DateTime.Now;
            ViewBag.blogCountLastSevenDays = _blogService.GetAllBL()
            .Count(x => x.BlogCreateDate >= lastSevenDays && x.BlogCreateDate <= now);

            return View();
        }
    }
}
