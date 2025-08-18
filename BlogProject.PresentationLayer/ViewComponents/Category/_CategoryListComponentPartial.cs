using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.ViewComponents.Category
{
    public class _CategoryListComponentPartial:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;

        public _CategoryListComponentPartial(ICategoryService categoryService, IBlogService blogService)
        {
            _categoryService = categoryService;
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _categoryService.GetAllBL();
            var count = values.ToDictionary(
                x => x.CategoryId,
                x => _blogService.List(z => z.CategoryId == x.CategoryId).Count
                );
            ViewBag.categoryBlogCounts = count;
            return View(values);
        }
    }
}
