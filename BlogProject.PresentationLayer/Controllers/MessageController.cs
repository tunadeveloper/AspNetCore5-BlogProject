using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BlogProject.PresentationLayer.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IActionResult InBox()
        {
            int id = 2;
            var values = _messageService.GetInboxListByWriterBL(id);
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var values = _messageService.GetInboxListByWriterBL(2);
            var message = values.FirstOrDefault(x => x.MessageId == id);
            return View(message);
        }
    }
}
