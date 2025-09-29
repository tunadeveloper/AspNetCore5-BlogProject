using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X.PagedList;
using X.PagedList.Extensions;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;

        public WriterController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public IActionResult Index(int page = 1)
        {
            var values = _writerService.GetAllBL().ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateWriter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateWriter(Writer writer)
        {
            if (ModelState.IsValid)
            {
                writer.WriterStatus = true;
                _writerService.InsertBL(writer);
                return RedirectToAction("Index");
            }
            return View(writer);
        }

        [HttpGet]
        public IActionResult UpdateWriter(int id)
        {
            var value = _writerService.GetByIdBL(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateWriter(Writer writer)
        {
            if (ModelState.IsValid)
            {
                _writerService.UpdateBL(writer);
                return RedirectToAction("Index");
            }
            return View(writer);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, bool status)
        {
            var writer = _writerService.GetByIdBL(id);
            if (writer == null)
                return Json(new { success = false });

            writer.WriterStatus = status;
            _writerService.UpdateBL(writer);

            return Json(new { success = true, newStatus = status });
        }

        public IActionResult DeleteWriter(int id)
        {
            var value = _writerService.GetByIdBL(id);
            _writerService.DeleteBL(value);
            return RedirectToAction("Index");
        }

        public IActionResult WriterDetails(int id)
        {
            var writer = _writerService.GetByIdBL(id);
            return View(writer);
        }
    }
}
