using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.ViewComponents.Blog
{
    public class _BlogListDashboardComponentPartial:ViewComponent
    {
        private readonly IBlogService _blogService;

        public _BlogListDashboardComponentPartial(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.GetListWithCategoryBL().OrderByDescending(x=>x.BlogId).Take(5).ToList();
            return View(values);
        }
}
}
