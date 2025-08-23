using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.ViewComponents
{
    public class _FooterComponentPartial:ViewComponent
    {
        private readonly IBlogService _blogService;

        public _FooterComponentPartial(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke() {
            var values = _blogService.GetLastBlogBL();
            return View(values);
        }
    }
}
