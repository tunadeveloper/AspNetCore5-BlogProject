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
    [Authorize(AuthenticationSchemes = "WriterAuth")]
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

        public async Task<IActionResult> InBox()
        {
            var user = await _userManager.GetUserAsync(User);
            int writerId = 0;
            
            if (user != null)
            {
                writerId = user.Id;
            }
            else
            {
                // WriterAuth scheme ile giriş yapmış kullanıcı için
                var userMail = User.Identity.Name;
                var writer = _writerService.List(x => x.WriterEmail == userMail).FirstOrDefault();
                writerId = writer != null ? writer.WriterId : 0;
            }
            
            var values = _messageService.GetInboxListByWriterBL(writerId);
            return View(values);
        }

        public async Task<IActionResult> MessageDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            int writerId = 0;
            
            if (user != null)
            {
                writerId = user.Id;
            }
            else
            {
                // WriterAuth scheme ile giriş yapmış kullanıcı için
                var userMail = User.Identity.Name;
                var writer = _writerService.List(x => x.WriterEmail == userMail).FirstOrDefault();
                writerId = writer != null ? writer.WriterId : 0;
            }

            var values = _messageService.GetInboxListByWriterBL(writerId);
            var message = values.FirstOrDefault(x => x.MessageId == id);
            return View(message);
        }
    }
}
