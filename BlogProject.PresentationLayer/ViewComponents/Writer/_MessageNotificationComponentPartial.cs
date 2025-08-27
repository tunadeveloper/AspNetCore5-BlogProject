using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.PresentationLayer.ViewComponents.Writer
{
    public class _MessageNotificationComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;

        public _MessageNotificationComponentPartial(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IViewComponentResult Invoke()
        {
            int id = 2;
            var values = _messageService.GetInboxListByWriterBL(id);
            return View(values);
        }
    }
}
