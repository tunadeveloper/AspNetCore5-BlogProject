using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2:ViewComponent
    {
        private readonly IBlogService _blogService;

        public Statistic2(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.LastBlog = _blogService.GetAllBL().OrderByDescending(x => x.BlogId).Take(1).Select(x => x.BlogTitle).FirstOrDefault();
            return View();
        }
    }
}
