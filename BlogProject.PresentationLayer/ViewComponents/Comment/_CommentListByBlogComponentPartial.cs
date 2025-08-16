using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.ViewComponents.Comment
{
    public class _CommentListByBlogComponentPartial:ViewComponent
    {
        private readonly  ICommentService _commentService;

        public _CommentListByBlogComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _commentService.List(x=>x.BlogId == 1);
            return View(values);
        }
    }
}
