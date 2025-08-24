using BlogProject.BusinessLayer.Abstract;
using BlogProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.ViewComponents.Category
{
    public class _DashboardCategoryListComponentPartial:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;

        public _DashboardCategoryListComponentPartial(ICategoryService categoryService, IBlogService blogService)
        {
            _categoryService = categoryService;
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _categoryService.GetAllBL();
            var categoryBlogCounts = values.Select(c => new CategoryBlogCountViewModel
            {
                CategoryName = c.CategoryName,
                BlogCount = _blogService.GetAllBL().Count(b => b.CategoryId == c.CategoryId)
            }).ToList();

            ViewBag.TotalBlogCount = _blogService.GetAllBL().Count();
            return View(categoryBlogCounts);
        }
    }
}
