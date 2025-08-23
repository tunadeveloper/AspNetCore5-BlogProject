using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogProject.PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public PartialViewResult CommentSendPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.CommentStatus = true;
            comment.BlogId = 1;
            _commentService.InsertBL(comment);
            return RedirectToAction("Index","Blog");
        }
    }
}
