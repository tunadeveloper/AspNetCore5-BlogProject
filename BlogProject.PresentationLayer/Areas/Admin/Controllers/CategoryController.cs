using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X.PagedList;
using X.PagedList.Extensions;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(int page = 1)
        {
            var values = _categoryService.GetAllBL().ToPagedList(page, 10);
            return View(values);
        }
        [HttpPost]
        public IActionResult ChangeStatus(int id, bool status)
        {
            var category = _categoryService.GetByIdBL(id);
            if (category == null)
                return Json(new { success = false });

            category.CategoryStatus = status;
            _categoryService.UpdateBL(category);

            return Json(new { success = true, newStatus = status });
        }


        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _categoryService.GetByIdBL(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateBL(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }


        public IActionResult CreateCategory() => View();

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            category.CategoryStatus = true;
            _categoryService.InsertBL(category);
            return View("Index", "Cateogory");
        }

        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.GetByIdBL(id);
            _categoryService.DeleteBL(value);
            return RedirectToAction("Index", "Category");
        }
    }
}
