using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BlogProject.PresentationLayer.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic3: ViewComponent
    {
        private readonly IBlogService _blogService;
        private readonly IWriterService _writerService;
        private readonly ICategoryService _categoryService;
        private readonly INewsletterService _newsletterService;
        private readonly IContactService _contactService;

        public Statistic3(IBlogService blogService, IWriterService writerService, ICategoryService categoryService, INewsletterService newsletterService, IContactService contactService)
        {
            _blogService = blogService;
            _writerService = writerService;
            _categoryService = categoryService;
            _newsletterService = newsletterService;
            _contactService = contactService;
        }

        public IViewComponentResult Invoke()
        {
            var allBlogs = _blogService.GetAllBL();
            var lastSevenDays = DateTime.Now.AddDays(-7);
            
            ViewBag.ActiveBlogCount = allBlogs.Count(x => x.BlogStatus == true);
            ViewBag.WriterCount = _writerService.GetAllBL().Count(x => x.WriterStatus == true);
            ViewBag.CategoryCount = _categoryService.GetAllBL().Count(x => x.CategoryStatus == true);
            ViewBag.NewsletterCount = _newsletterService.GetAllBL().Count(x => x.EmailStaus == true);
            ViewBag.LastWeekBlogCount = allBlogs.Count(x => x.BlogCreateDate >= lastSevenDays);
            
            var lastMessages = _contactService.GetAllBL()
                .OrderByDescending(x => x.ContactDate)
                .Take(5)
                .ToList();
            ViewBag.LastMessages = lastMessages;

            return View();
        }
    }
}
