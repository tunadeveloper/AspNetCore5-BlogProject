using BlogProject.BusinessLayer.Abstract;
using BlogProject.BusinessLayer.ValidationRules;
using BlogProject.EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;
        private readonly BlogValidator _blogValidator;
        public BlogController(IBlogService blogService, ICommentService commentService, ICategoryService categoryService, BlogValidator validator)
        {
            _blogService = blogService;
            _commentService = commentService;
            _categoryService = categoryService;
            _blogValidator = validator;
        }

        public IActionResult Index()
        {
            var values = _blogService.GetListWithCategoryBL();
            return View(values);
        }

        public IActionResult BlogDetails(int id)
        {
            ViewBag.id = id;
            var values = _blogService.GetByIdWithCategoryBL(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()
        {
            var values = _blogService.GetBlogListByWriterBL(1);
            return View(values);
        }

        public IActionResult CreateBlog()
        {
            List<SelectListItem> categoryValues = (from x in _categoryService.GetAllBL()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }
                                                   ).ToList();
            ViewBag.Categories = categoryValues;
            return View();
        }

        [HttpPost]
        public IActionResult CreateBlog(Blog blog)
        {
            var result = _blogValidator.Validate(blog);
            if (result.IsValid)
            {
                blog.BlogCreateDate = System.DateTime.Now;
                blog.WriterId = 1;
                blog.BlogStatus = true;
                _blogService.InsertBL(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                List<SelectListItem> categoryValues = (from x in _categoryService.GetAllBL()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.CategoryName,
                                                           Value = x.CategoryId.ToString()
                                                       }).ToList();
                ViewBag.Categories = categoryValues;
                return View(blog);
            }
        }

        public IActionResult UpdateBlog(int id)
        {
            var values = _blogService.GetByIdBL(id);
            List<SelectListItem> categoryValues = (from x in _categoryService.GetAllBL()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }
                                                   ).ToList();

            List<SelectListItem> statusValues = new List<SelectListItem> {
                new SelectListItem { Text = "Aktif", Value = "true", Selected = values.BlogStatus },
                new SelectListItem { Text = "Pasif", Value = "false", Selected = !values.BlogStatus }
                };
            ViewBag.Categories = categoryValues;
            ViewBag.Status = statusValues;
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateBlog(Blog blog)
        {
            var result = _blogValidator.Validate(blog);
            if (result.IsValid)
            {
                blog.BlogCreateDate = System.DateTime.Now;
                blog.WriterId = 1;
                _blogService.UpdateBL(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                List<SelectListItem> categoryValues = (from x in _categoryService.GetAllBL()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.CategoryName,
                                                           Value = x.CategoryId.ToString()
                                                       }).ToList();
                ViewBag.Categories = categoryValues;
                return View(blog);
            }
        }

        public IActionResult DeleteBlog(int id)
        {
            var values = _blogService.GetByIdBL(id);
            _blogService.DeleteBL(values);
            return RedirectToAction("BlogListByWriter", "Blog");
        }
    }
}
