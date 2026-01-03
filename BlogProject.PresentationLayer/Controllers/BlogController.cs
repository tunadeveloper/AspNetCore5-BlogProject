using BlogProject.BusinessLayer.Abstract;
using BlogProject.BusinessLayer.ValidationRules;
using BlogProject.DataAccessLayer.Concrete;
using BlogProject.EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;
        private readonly BlogValidator _blogValidator;
        private readonly Context _context;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public BlogController(IBlogService blogService, ICommentService commentService, ICategoryService categoryService, BlogValidator validator, Context context, UserManager<IdentityUser<int>> userManager)
        {
            _blogService = blogService;
            _commentService = commentService;
            _categoryService = categoryService;
            _blogValidator = validator;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(int? categoryId, int page = 1)
        {
            var allBlogs = _blogService.GetListWithCategoryBL().Where(x => x.BlogStatus == true).ToList();
            
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                allBlogs = allBlogs.Where(x => x.CategoryId == categoryId.Value).ToList();
            }
            
            var pagedBlogs = allBlogs.ToPagedList(page, 9);
            
            ViewBag.Categories = _categoryService.GetAllBL().Where(x => x.CategoryStatus == true).ToList();
            ViewBag.SelectedCategoryId = categoryId;
            
            return View(pagedBlogs);
        }

        public IActionResult BlogDetails(int id)
        {
            ViewBag.id = id;
            var values = _blogService.GetByIdWithCategoryBL(id);
            
            if (values == null)
            {
                return NotFound();
            }
            
            return View(values);
        }

        [Authorize(AuthenticationSchemes = "WriterAuth")]
        public async Task<IActionResult> BlogListByWriter()
        {
            var user = await _userManager.GetUserAsync(User);
            int writerID = 0;
            
            if (user != null)
            {
                writerID = user.Id;
            }
            else
            {
                var userEmail = User.Identity.Name;
                writerID = _context.Writers.Where(x => x.WriterEmail == userEmail).Select(x => x.WriterId).FirstOrDefault();
            }
            
            var values = _blogService.GetBlogListByWriterBL(writerID);
            return View(values);
        }

        [Authorize(AuthenticationSchemes = "WriterAuth")]
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
        public async Task<IActionResult> CreateBlog(Blog blog)
        {
            var user = await _userManager.GetUserAsync(User);
            int writerID = 0;
            
            if (user != null)
            {
                writerID = user.Id;
            }
            else
            {
                var userEmail = User.Identity.Name;
                writerID = _context.Writers.Where(x => x.WriterEmail == userEmail).Select(x => x.WriterId).FirstOrDefault();
            }
            
            var result = _blogValidator.Validate(blog);
            if (result.IsValid)
            {
                blog.BlogCreateDate = System.DateTime.Now;
                blog.WriterId = writerID;
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

        [Authorize(AuthenticationSchemes = "WriterAuth")]
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
        public async Task<IActionResult> UpdateBlog(Blog blog)
        {
            var user = await _userManager.GetUserAsync(User);
            int writerID = 0;
            
            if (user != null)
            {
                writerID = user.Id;
            }
            else
            {
                var userEmail = User.Identity.Name;
                writerID = _context.Writers.Where(x => x.WriterEmail == userEmail).Select(x => x.WriterId).FirstOrDefault();
            }
            
            var result = _blogValidator.Validate(blog);
            if (result.IsValid)
            {
                blog.BlogCreateDate = System.DateTime.Now;
                blog.WriterId = writerID;
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

        [Authorize(AuthenticationSchemes = "WriterAuth")]
        public IActionResult DeleteBlog(int id)
        {
            var values = _blogService.GetByIdBL(id);
            _blogService.DeleteBL(values);
            return RedirectToAction("BlogListByWriter", "Blog");
        }
    }
}
