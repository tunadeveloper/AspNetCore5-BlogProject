using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Principal;

namespace BlogProject.PresentationLayer.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;

        public BlogController(IBlogService blogService, ICommentService commentService)
        {
            _blogService = blogService;
            _commentService = commentService;
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
    }
}
