using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.PresentationLayer.Areas.Admin.ViewComponents.Statistic
{
    public class LastMessages : ViewComponent
    {
        private readonly IContactService _contactService;

        public LastMessages(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IViewComponentResult Invoke()
        {
            var lastMessages = _contactService.GetAllBL()
                .OrderByDescending(x => x.ContactDate)
                .Take(5)
                .ToList();
            return View(lastMessages);
        }
    }
}

