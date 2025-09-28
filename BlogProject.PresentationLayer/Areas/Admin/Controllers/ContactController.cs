using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var values = _contactService.GetByIdBL(1);
            return View(values);
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            _contactService.UpdateBL(contact);
            return RedirectToAction("Index");
        }
    }
}
