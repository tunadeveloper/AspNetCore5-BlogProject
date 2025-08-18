using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.ViewComponents.Blog
{
    public class _WriterLastBlogComponentPartial:ViewComponent
    {
        private readonly IBlogService _blogService;

        public _WriterLastBlogComponentPartial(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.GetBlogListByWriterBL(1);
            return View(values);
        }
    }
}
