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
            var lastBlogs = _blogService.GetListWithCategoryBL()
                .OrderByDescending(x => x.BlogId)
                .Take(7)
                .ToList();
            ViewBag.LastBlogs = lastBlogs;
            return View();
        }
    }
}
