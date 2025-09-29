using BlogProject.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public MessageController(IMessageService messageService, UserManager<IdentityUser<int>> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }

        public async Task<IActionResult> InBox()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int writerId = user.Id;
            var values = _messageService.GetInboxListByWriterBL(writerId);
            return View(values);
        }

        public async Task<IActionResult> MessageDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int writerId = user.Id;
            var values = _messageService.GetInboxListByWriterBL(writerId);
            var message = values.FirstOrDefault(x => x.MessageId == id);
            return View(message);
        }
    }
}
