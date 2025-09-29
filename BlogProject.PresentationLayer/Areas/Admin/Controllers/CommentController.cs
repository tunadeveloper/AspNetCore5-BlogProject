using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            var values = _commentService.GetCommentWithBlog();
            return View(values);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, bool status)
        {
            var comment = _commentService.GetByIdBL(id);
            if (comment == null)
                return Json(new { success = false });

            comment.CommentStatus = status;
            _commentService.UpdateBL(comment);

            return Json(new { success = true, newStatus = status });
        }

        public IActionResult DeleteComment(int id)
        {
            var comment = _commentService.GetByIdBL(id);
            _commentService.DeleteBL(comment);
            return RedirectToAction("Index");
        }

    }
}
