using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace BlogProject.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IWriterService _writerService;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public MessageController(IMessageService messageService, IWriterService writerService, UserManager<IdentityUser<int>> userManager)
        {
            _messageService = messageService;
            _writerService = writerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            int adminId = user?.Id ?? 1;
            
            var allMessages = _messageService.GetAllBL()
                .Where(x => x.SenderId == adminId || x.ReceiverId == adminId)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
            
            var values = allMessages.ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateMessage()
        {
            var writers = _writerService.GetAllBL();
            ViewBag.Writers = writers;

            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUserId = currentUser?.Id ?? 1;

            return View(new Message2());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMessage(Message2 message)
        {
            if (!int.TryParse(Request.Form["ReceiverId"], out int receiverId))
            {
                ModelState.AddModelError("ReceiverId", "Alıcı seçiniz.");
            }
            else
            {
                message.ReceiverId = receiverId;
            }

            var currentUser = await _userManager.GetUserAsync(User);
            message.SenderId = currentUser?.Id ?? 1;

            message.CreatedDate = System.DateTime.Now;
            message.Status = true;

            if (ModelState.IsValid)
            {
                _messageService.InsertBL(message);
                return RedirectToAction("Index", "Message", new { area = "Admin" });
            }

            ViewBag.Writers = _writerService.GetAllBL();
            return View(message);
        }
    

        [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            var value = _messageService.GetByIdBL(id);
            var writers = _writerService.GetAllBL();
            ViewBag.Writers = writers;
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateMessage(Message2 message)
        {
            if (ModelState.IsValid)
            {
                var existingMessage = _messageService.GetByIdBL(message.MessageId);
                message.SenderId = existingMessage.SenderId;
                
                _messageService.UpdateBL(message);
                return RedirectToAction("Index");
            }
            var writers = _writerService.GetAllBL();
            ViewBag.Writers = writers;
            return View(message);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, bool status)
        {
            var message = _messageService.GetByIdBL(id);
            if (message == null)
                return Json(new { success = false });

            message.Status = status;
            _messageService.UpdateBL(message);

            return Json(new { success = true, newStatus = status });
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _messageService.GetByIdBL(id);
            _messageService.DeleteBL(value);
            return RedirectToAction("Index");
        }

        public IActionResult MessageDetails(int id)
        {
            var message = _messageService.GetByIdBL(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        public IActionResult Inbox(int id, int page = 1)
        {
            var messages = _messageService.GetInboxListByWriterBL(id).ToPagedList(page, 10);
            return View(messages);
        }
    }
}
