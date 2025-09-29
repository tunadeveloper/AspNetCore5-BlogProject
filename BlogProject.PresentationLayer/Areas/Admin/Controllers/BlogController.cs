using BlogProject.BusinessLayer.Abstract;
using BlogProject.DataAccessLayer.Concrete;
using BlogProject.EntityLayer.Concrete;
using BlogProject.PresentationLayer.Areas.Admin.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X.PagedList;
using X.PagedList.Extensions;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;

        public BlogController(IBlogService blogService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }
        public IActionResult ExportStaticExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int blogRowCount = 2;
                foreach (var item in GetBlogList())
                {
                    worksheet.Cell(blogRowCount, 1).Value = item.Id;
                    worksheet.Cell(blogRowCount, 2).Value = item.BlogName;
                    blogRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "work1.xlsx");
                }
            }
        }

        private List<BlogModel> GetBlogList()
        {
            List<BlogModel> bM = new List<BlogModel>
            {
                new BlogModel{Id=1, BlogName="C# Programlamaya Giriş"},
                new BlogModel{Id=2, BlogName = "Tesla firmasının araçları"}
        };
            return bM;
        }

        public IActionResult BlogListExcel()
        {
            return View();
        }

        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int blogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(blogRowCount, 1).Value = item.Id;
                    worksheet.Cell(blogRowCount, 2).Value = item.BlogName;
                    blogRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "work1.xlsx");
                }
            }
        }

        private List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> bM = new List<BlogModel2>();
            using (var context = new Context())
            {
                bM = context.Blogs.Select(x => new BlogModel2
                {
                    Id = x.BlogId,
                    BlogName = x.BlogTitle
                }).ToList();
            }
            return bM;
        }

        public IActionResult BlogTitleListExcel()
        {
            return View();
        }

        public IActionResult Index(int page = 1)
        {
            var values = _blogService.GetListWithCategoryBL().ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateBlog()
        {
            ViewBag.Categories = _categoryService.GetAllBL();
            return View();
        }

        [HttpPost]
        public IActionResult CreateBlog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.BlogStatus = true;
                blog.BlogCreateDate = System.DateTime.Now;
                _blogService.InsertBL(blog);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryService.GetAllBL();
            return View(blog);
        }

        [HttpGet]
        public IActionResult UpdateBlog(int id)
        {
            var blog = _blogService.GetByIdWithCategoryBL(id);
            ViewBag.Categories = _categoryService.GetAllBL();
            return View(blog);
        }

        [HttpPost]
        public IActionResult UpdateBlog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _blogService.UpdateBL(blog);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryService.GetAllBL();
            return View(blog);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, bool status)
        {
            var blog = _blogService.GetByIdBL(id);
            if (blog == null)
                return Json(new { success = false });

            blog.BlogStatus = status;
            _blogService.UpdateBL(blog);

            return Json(new { success = true, newStatus = status });
        }

        public IActionResult DeleteBlog(int id)
        {
            var value = _blogService.GetByIdBL(id);
            _blogService.DeleteBL(value);
            return RedirectToAction("Index");
        }

        public IActionResult BlogDetails(int id)
        {
            var blog = _blogService.GetByIdWithCategoryBL(id);
            return View(blog);
        }
    }
}
