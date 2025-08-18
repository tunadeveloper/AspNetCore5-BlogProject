using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.ViewComponents.Comment
{
    public class _CommentListByBlogComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _CommentListByBlogComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var values = _commentService.List(x => x.BlogId == id);
            if (values == null || !values.Any())
            {
                ViewBag.noComments = "Henüz bu blog için yorum yapılmamış.";
            }
            return View(values);
        }
    }
}
